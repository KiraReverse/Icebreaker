using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : BaseInteractableController
{
    GameManager gameManager;
    public Transform entryPoint;
    public TeleportController connectedTP;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    public override void Interact()
    {
        gameManager.ActivateNextObjective(this.gameObject);
        StartCoroutine(Teleport());
    }


    IEnumerator Teleport()
    {
        gameManager.StartFade(1f, 0.5f);
        player.GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        player.transform.position = connectedTP.entryPoint.position;

        yield return new WaitForSeconds(0.5f);

        gameManager.StartFade(0f, 0.5f);
        player.GetComponent<PlayerController>().enabled = true;
    }
}
