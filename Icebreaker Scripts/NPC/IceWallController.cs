using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallController : BaseInteractableController
{
    public bool isFree;

    ActionCountManager acManager;
    PlayerController playerCon;

    SpriteRenderer sr;
    Material mat;

    bool isMelted = false;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        acManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ActionCountManager>();
        playerCon = player.GetComponent<PlayerController>();

        sr = GetComponentInChildren<SpriteRenderer>();
        mat = sr.material;
    }

    public override void Interact()
    {
        if (playerCon.isZork)
        {
            base.Interact();
        }

        else
        {
            if (!isFree && acManager != null &&!isMelted)
            {
                acManager.Stabbed();
                isMelted = true;
            }
            StartCoroutine(Melt());
        }
    }

    public void Blasted(FireballController fbController)
    {
        if(acManager != null && fbController.iceWallHitCount <= 0 && !isFree)
        {
            acManager.Fireballed();
        }
        isMelted = true;
        StartCoroutine(Melt());
    }

    IEnumerator Melt()
    {
        float temp = 1f;
        isActive = false;

        while (temp > 0f)
        {
            temp -= Time.deltaTime;
            mat.SetFloat("_Fade", temp);
            yield return null;
        }

        gameObject.SetActive(false);


    }
}

