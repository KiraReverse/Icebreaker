using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeController : BaseInteractableController
{
    GameManager gameManager;


    // Start is called before the first frame update
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

        StartCoroutine(SwapClothes());
    }

    IEnumerator SwapClothes()
    {
        gameManager.StartFade(1f, 0.5f);
        player.GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        gameManager.SwapToArmour();
        yield return new WaitForSeconds(0.5f);

        gameManager.StartFade(0f, 0.5f);

     

        player.GetComponent<PlayerController>().enabled = true;
        base.Interact();
        gameManager.ActivateNextObjective(this.gameObject);
    }
}
