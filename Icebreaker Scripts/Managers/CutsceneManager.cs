using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(DialogueManager))]

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Animation Controllers", order = 1)]
    public List<BaseCutsceneController> cutscenes = new List<BaseCutsceneController>();

    [Header("Cutscene Dialogue Containers", order = 2)]
    public List<BaseContainer> dialogueList = new List<BaseContainer>();

    [Header("Cutscene Triggers", order = 3)]
    public List<GameObject> triggers = new List<GameObject>();

    GameManager gameManager;
    DialogueManager dialogueManager;

    public bool cutsceneOngoing = false;
    public bool dialogueOngoing = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        dialogueManager = GetComponent<DialogueManager>();

        if (triggers.Count > 0)
        {
            foreach (GameObject t in triggers)
            {
                t.SetActive(false);
            }

            triggers[0].SetActive(true);
        }
    }

    public void PlayNextCutscene()
    {
        StartCoroutine(NextCutscene());
    }

    IEnumerator NextCutscene()
    {

        if (dialogueList.Count > 0)
        {
            if (cutscenes[0] != null && !cutscenes[0].afterDialogue)
            {
                cutscenes[0].PlayCutscene();
                yield return new WaitWhile(() => cutsceneOngoing);
            }


            if (dialogueList[0] != null)
            {
                dialogueManager.SortContainer(dialogueList[0]);
                yield return new WaitWhile(() => dialogueOngoing);
            }

            if (cutscenes[0] != null && cutscenes[0].afterDialogue)
            {
                cutscenes[0].PlayCutscene();
                yield return new WaitWhile(() => cutsceneOngoing);
            }

         


            cutscenes.RemoveAt(0);
            dialogueList.RemoveAt(0);
            triggers.RemoveAt(0);

            if (triggers.Count > 0)
            {
                triggers[0].SetActive(true);
            }

            gameManager.CheckForExit();
        }

        yield return null;
    }

    public void EndCutscene()
    {
        gameManager.GameState = Enums.GameState.playing;
    }
}
