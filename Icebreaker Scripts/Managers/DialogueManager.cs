using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

[RequireComponent(typeof(GameManager))]
public class DialogueManager : MonoBehaviour
{
    public Color32 cPlayerName;
    public Color32 cZorkName;
    public Color32 cRamuName;
    public Color32 cVillagerName;

    public Button choice1;
    TextMeshProUGUI choice1Text;

    public Button choice2;
    TextMeshProUGUI choice2Text;

    GameObject cutsceneBox;
    TextMeshProUGUI cutsceneText;

    GameObject nameBox;
    TextMeshProUGUI nameText;

    GameObject player;
    GameManager gameManager;
    PlayerConfig playerConfig;

    ChoiceManager choiceManager;
    CutsceneManager cutsceneManager;

    KeyCode interactKey;

    bool buttonOneDown;
    bool buttonTwoDown;


    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GetComponent<GameManager>();

        choice1Text = choice1.GetComponentInChildren<TextMeshProUGUI>();
        choice2Text = choice2.GetComponentInChildren<TextMeshProUGUI>();

        choiceManager = GetComponent<ChoiceManager>();
        cutsceneManager = GetComponent<CutsceneManager>();

        cutsceneBox = GameObject.FindGameObjectWithTag("Dialogue Box");

        cutsceneText = cutsceneBox.GetComponentInChildren<TextMeshProUGUI>();
        nameBox = GameObject.FindGameObjectWithTag("Name Box");
        nameText = nameBox.GetComponentInChildren<TextMeshProUGUI>();


        player = GameObject.FindGameObjectWithTag("Player");

        playerConfig = gameManager.playerConfig;

        interactKey = playerConfig.interactKey;

    }

    public void SortContainer(BaseContainer container)
    {
        if (container as DialogueContainer != null)
        {
            StartCoroutine(ShowText((DialogueContainer)container));
        }

        else if(container as ChoiceContainer != null)
        {
            StartCoroutine(ShowChoice((ChoiceContainer)container));
        }

        else if(container as SplitDialogueContainer != null)
        {
            SplitDialogueContainer c = (SplitDialogueContainer)container;
            
            switch(choiceManager.CheckChoice(c.choiceNumber))
            {
                default:
                case 1:
                    StartCoroutine(ShowText(c.result1));
                    break;

                case 2:
                    StartCoroutine(ShowText(c.result2));
                    break;
            }
        }
    }

    IEnumerator ShowChoice(ChoiceContainer cContainer)
    {
        float startTime = Time.time;
        gameManager.GameState = Enums.GameState.cutscene;
        cutsceneManager.dialogueOngoing = true;
        player.GetComponent<PlayerController>().enabled = false;

        yield return null;
        string name = GetSpeakerName(cContainer.speaker);

        if (name != null)
        {
            switch(cContainer.speaker)
            {
                case Speakers.Zork:
                    nameText.color = cZorkName;
                    break;
                case Speakers.Ramu:
                    nameText.color = cRamuName;
                    break;
                case Speakers.Villager:
                    nameText.color = cVillagerName;
                    break;
                case Speakers.Player:
                    nameText.color = cPlayerName;
                    break;
            }

            nameText.text = name;
            nameBox.SetActive(true);

        }

        if (playerConfig.gameMode != GameMode.agencyNegative)
        {
            choice1Text.text = cContainer.option1;
            choice2Text.text = cContainer.option2;

            
        }

        else
        {
            choice1Text.text = cContainer.option2;
            choice2Text.text = cContainer.option1;
        }

        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);

        cutsceneText.text = cContainer.question;
        cutsceneBox.SetActive(true);

        yield return new WaitUntil(() => buttonOneDown || buttonTwoDown);

        player.GetComponent<PlayerController>().enabled = true;
        gameManager.GameState = Enums.GameState.playing;
        cutsceneManager.dialogueOngoing = false;
        cutsceneBox.SetActive(false);
        nameBox.SetActive(false);
         
        if (buttonOneDown)
        {
            choiceManager.RecordChoice(cContainer.choiceNumber,"1", (Time.time - startTime).ToString());
            buttonOneDown = false;
        }
        else
        {
            choiceManager.RecordChoice(cContainer.choiceNumber,"2", (Time.time - startTime).ToString());
            buttonTwoDown = false;
        }

        

    }

    IEnumerator ShowText(DialogueContainer dContainer)
    {
        if (dContainer is null)
        {
            Debug.Log("container null");
        }

        gameManager.GameState = Enums.GameState.cutscene;
        cutsceneManager.dialogueOngoing = true;
        player.GetComponent<PlayerController>().enabled = false;

        DialogueLine[] dLines = dContainer.dialogueLines.ToArray();

        cutsceneBox.SetActive(true);


        foreach (DialogueLine d in dLines)
        {
            string name = GetSpeakerName(d.Speaker);

            if (name != null)
            {
                switch (d.Speaker)
                {
                    case Speakers.Zork:
                        nameText.color = cZorkName;
                        break;
                    case Speakers.Ramu:
                        nameText.color = cRamuName;
                        break;
                    case Speakers.Villager:
                        nameText.color = cVillagerName;
                        break;
                    case Speakers.Player:
                        nameText.color = cPlayerName;
                        break;
                }

                nameText.text = name;
                nameBox.SetActive(true);

            }

            else
            {
                nameBox.SetActive(false);
            }

            cutsceneText.text = d.GetDialogueLine(playerConfig.gameMode);

            if (cutsceneText.text != "" || cutsceneText.text == null)
            {
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(interactKey));
            }
            
        }

        cutsceneBox.SetActive(false);
        nameBox.SetActive(false);
        cutsceneManager.dialogueOngoing = false;
        player.GetComponent<PlayerController>().enabled = true;
        gameManager.GameState = Enums.GameState.playing;
    }

    string GetSpeakerName(Speakers s)
    {
        switch(s)
        {
            case Speakers.Player:
                return playerConfig.playerName;

            case Speakers.Zork:
                return "Zork";

            case Speakers.Villager:
                return "Villager";

            case Speakers.Ramu:
                return "Ramu";

            case Speakers.Narrator:
                return null;
        }

        return null;
    }


    public void ButtonOneDown()
    {
        buttonOneDown = true;
    }

    public void ButtonTwoDown()
    {
        buttonTwoDown = true;
    }

}
