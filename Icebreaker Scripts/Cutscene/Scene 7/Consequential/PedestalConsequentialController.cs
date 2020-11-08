﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalConsequentialController : BaseInteractableController
{
    public GameObject door;
    public CinemachineVirtualCamera vcam;
    public GameObject partner;
    GameManager gameManager;
    ChoiceManager choiceManager;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        choiceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ChoiceManager>();

        if(choiceManager.CheckChoice(7) != 1)
        {
            player.GetComponent<PlayerController>().hasPartner = false;
            partner.gameObject.SetActive(false);
        }

    }


    public override void Interact()
    {
        if (!isActive)
        {
            return;
        }

        StartCoroutine(WatchDoor());
        gameManager.ActivateNextObjective(this.gameObject);
    }



    IEnumerator WatchDoor()
    {

        if (choiceManager.CheckChoice(7) == 1)
        {

            gameManager.GameState = Enums.GameState.cutscene;

            base.Interact();

            yield return new WaitUntil(() => Input.GetKeyDown(gameManager.playerConfig.interactKey));
            yield return new WaitUntil(() => Input.GetKeyDown(gameManager.playerConfig.interactKey));
            yield return new WaitUntil(() => Input.GetKeyDown(gameManager.playerConfig.interactKey));

            SpriteRenderer doorSr = door.GetComponentInChildren<SpriteRenderer>();

            doorSr.color = new Color(doorSr.color.r, doorSr.color.g, doorSr.color.b, 1f);
 
            vcam.Follow = door.transform;

            yield return new WaitForSeconds(2f);


            vcam.Follow = player.transform;

            yield return new WaitForSeconds(0.5f);

            gameManager.GameState = Enums.GameState.playing;


            
        }

        else
        {
            gameManager.GameState = Enums.GameState.cutscene;
            vcam.Follow = door.transform;

            yield return new WaitForSeconds(1f);

            float temp = 0f;

            SpriteRenderer doorSr = door.GetComponentInChildren<SpriteRenderer>();
            while (temp < 1)
            {
                temp += 0.005f;

                doorSr.color = new Color(doorSr.color.r, doorSr.color.g, doorSr.color.b, temp);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);

            vcam.Follow = player.transform;

            yield return new WaitForSeconds(0.5f);

            gameManager.GameState = Enums.GameState.playing;


            base.Interact();
        }
    }
}
