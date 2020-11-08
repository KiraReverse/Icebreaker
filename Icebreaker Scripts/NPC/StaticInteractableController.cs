using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInteractableController : BaseInteractableController
{
    GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


    }



    public override void Interact()
    {
        if (!isActive)
        {
            return;
        }


        base.Interact();

        gameManager.ActivateNextObjective(this.gameObject);
    }
}
