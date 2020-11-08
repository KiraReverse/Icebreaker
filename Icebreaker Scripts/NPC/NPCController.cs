using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : BaseInteractableController
{
   

    public GameObject dotDotDot;

    public bool disableDot;

    public bool isObjective;

    GameManager gameManager;

    // Start is called before the first frame update
    protected override void Awake()
    {

        base.Awake();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Start()
    {
        if (!disableDot)
        {
            dotDotDot.SetActive(true);
        }
    }

    public override void Interact()
    {
        base.Interact();

        dotDotDot.SetActive(false);

        if(isObjective)
        {
            gameManager.ActivateNextObjective(this.gameObject);
        }
    }

}
