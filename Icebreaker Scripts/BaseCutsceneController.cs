using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCutsceneController : MonoBehaviour
{

    public bool afterDialogue = false;

    protected GameManager gameManager;
    protected CutsceneManager cutsceneManager;
    protected ChoiceManager choiceManager;
    protected GameObject player;

    protected void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cutsceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CutsceneManager>();
        choiceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ChoiceManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void PlayCutscene()
    {
        StartCoroutine(Cutscene());
    }

    protected abstract IEnumerator Cutscene();
}
