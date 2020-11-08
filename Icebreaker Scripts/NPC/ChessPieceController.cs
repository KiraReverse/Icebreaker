using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceController : BaseInteractableController
{

    public bool isFree;

    ActionCountManager acManager;
    CinemachineImpulseSource impulse;

    SpriteRenderer sr;
    Material mat;

    bool isMelted = false;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        acManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ActionCountManager>();

        sr = GetComponentInChildren<SpriteRenderer>();
        mat = sr.material;
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    public override void Interact()
    {
        base.Interact();
    }

    public void Blasted(FireballController fbController)
    {
        if (!isMelted)
        {
            isMelted = true;

            if (!isFree && fbController.iceWallHitCount<1)
            {
                acManager.Fireballed();
            }


            StartCoroutine(Melt());
        }
    }

    IEnumerator Melt()
    {
        float temp = 1f;
        isActive = false;
        impulse.GenerateImpulse();
        while (temp > 0f)
        {
            temp -= Time.deltaTime;
            mat.SetFloat("_Fade", temp);
            yield return null;
        }

        gameObject.SetActive(false);


    }
}
