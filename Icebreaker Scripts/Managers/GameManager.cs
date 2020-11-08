using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enums;

[RequireComponent(typeof(DialogueManager))]
[RequireComponent(typeof(CutsceneManager))]
public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.playing;
    public GameState GameState { get => gameState; set => gameState = value; }
    CutsceneManager cutsceneManager;

    [Header("Config Info", order = 1)]
    public PlayerConfig playerConfig;
    public ChoiceConfig choiceConfig;
    public bool startCasual;
    public RuntimeAnimatorController[] playerCasualAnimators;
    public RuntimeAnimatorController[] playerArmorAnimators;
    public float fadeTime;

    [Header("Objective List", order = 2)]
    public List<GameObject> objectivesList;
    public GameObject exit;
    GameObject currObjective;

    [Header("Object References", order = 3)]
    public GameObject player;
    public GameObject playerSprite;
    public GameObject partner;
    Transform objArrow;

    //internal counter settings
    int interactCounter = 0;
    int fireboltCounter = 0;
    int dashCounter = 0;


    Image fadeImage;

    DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Awake()
    {
        LoadPlayerInfo();
        
        fadeImage = GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<Image>();

        StartCoroutine(FadeTo(fadeImage, 0f, fadeTime));

        cutsceneManager = GetComponent<CutsceneManager>();

        objArrow = transform.GetChild(0);

        interactCounter = 0;
        fireboltCounter = 0;
        dashCounter = 0;
    }

    private void Start()
    {
        if (objectivesList.Count > 0)
        {
            foreach(GameObject c in objectivesList)
            {
                if (c.activeSelf)
                {
                    SetObjectiveActive(c, false);
                }
            }

            currObjective = objectivesList[0];

            SetObjectiveActive(currObjective, true);
        }

        else
        {
            currObjective = null;
            exit.GetComponent<ExitController>().SetActive(true);

            objArrow.position = exit.transform.position;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(playerConfig.interactKey))
        {
            ++interactCounter;
        }

        if(Input.GetKeyDown(playerConfig.fireballKey))
        {
            ++fireboltCounter;
        }

        if(Input.GetKeyDown(playerConfig.dodgeKey))
        {
            ++dashCounter;
        }
    }

    void SetObjectiveActive(GameObject obj, bool b)
    {
        if(b)
        {
            objArrow.position = obj.transform.position;
        }

        obj.GetComponent<BaseInteractableController>().SetActive(b);
    }    

    void LoadPlayerInfo()
    {
        if(playerCasualAnimators.Length <= 0)
        {
            Debug.Log("PC Casual Animator Selection List Empty!");
            return;
        }

        else if (playerArmorAnimators.Length <= 0)
        {
            Debug.Log("PC Armor Animator Selection List Empty!");
            return;
        }

        if (startCasual)
        {
            switch (playerConfig.playerType)
            {
                case PlayerType.PC1:
                default:
                    playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerCasualAnimators[0];
                    break;

                case PlayerType.PC2:
                    playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerCasualAnimators[1];
                    break;

                case PlayerType.PC3:
                    playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerCasualAnimators[2];
                    break;
            }
        }

        else
        {
            switch (playerConfig.playerType)
            {
                case PlayerType.PC1:
                default:
                    playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerArmorAnimators[0];
                    break;

                case PlayerType.PC2:
                    playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerArmorAnimators[1];
                    break;

                case PlayerType.PC3:
                    playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerArmorAnimators[2];
                    break;
            }
        }
    }

    public void LogSceneData(bool died)
    {
        choiceConfig.AddSceneCounter(SceneManager.GetActiveScene().name, interactCounter, fireboltCounter, dashCounter, Time.timeSinceLevelLoad, died);
    }

    public void SwapToArmour()
    {
        switch (playerConfig.playerType)
        {
            case PlayerType.PC1:
            default:
                playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerArmorAnimators[0];
                break;

            case PlayerType.PC2:
                playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerArmorAnimators[1];
                break;

            case PlayerType.PC3:
                playerSprite.GetComponent<Animator>().runtimeAnimatorController = playerArmorAnimators[2];
                break;
        }
    }


    public void ActivateNextObjective(GameObject check)
    {
        if (currObjective != null && currObjective == check)
        {
            if (!currObjective.activeSelf)
            {
                currObjective.SetActive(true);
            }

            SetObjectiveActive(currObjective, false);
        }
        else
        {
            return;
        }

        objectivesList.RemoveAt(0);

        if (objectivesList.Count <= 0)
        {
            currObjective = null;
            objArrow.gameObject.SetActive(false);
            CheckForExit();
            return;
        }

        currObjective = objectivesList[0];

        SetObjectiveActive(currObjective, true);
    }

    public void CheckForExit()
    {
        if(objectivesList.Count <= 0 && cutsceneManager.triggers.Count <=0)
        {
            objArrow.gameObject.SetActive(true);
            exit.GetComponent<ExitController>().SetActive(true);
            objArrow.position = exit.transform.position;
        }
    }

    public void StartFade(float to, float time)
    {
        StartCoroutine(FadeTo(fadeImage, to, time));
    }

    public void PlayerDied()
    {
        LogSceneData(true);
        StartCoroutine(PlayerDead());
    }
    IEnumerator PlayerDead()
    {
        gameState = GameState.cutscene;

        player.GetComponentInChildren<Animator>().SetBool("isDead", true);

        if (partner != null)
        {
            partner.GetComponentInChildren<Animator>().SetBool("isDead", true);
        }

        yield return new WaitForSeconds(1f);

        StartFade(1f, 1f);

        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator FadeTo(Image img, float to, float time)
    {

        float from = img.color.a;
        float timer = 0;

        while (timer < time)
        {

            from = Mathf.Lerp(from, to, timer);
            img.color = new Color(img.color.r, img.color.g, img.color.b, from);
            timer += Time.deltaTime;

            yield return null;
        }

    }
}


