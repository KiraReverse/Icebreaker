using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalController : BaseInteractableController
{
    public GameObject door;
    public CinemachineVirtualCamera vcam;
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


        StartCoroutine(WatchDoor());
        gameManager.ActivateNextObjective(this.gameObject);
    }

    IEnumerator WatchDoor()
    {
        gameManager.GameState = Enums.GameState.cutscene;
        vcam.Follow = door.transform;

        yield return new WaitForSeconds(0.5f);

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
