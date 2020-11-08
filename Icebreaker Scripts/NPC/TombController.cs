using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombController : BaseInteractableController
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;
    Material outline;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        outline = spriteRenderer.material;

        SetHighlight(false);

    }

    public void SetHighlight(bool active)
    {
        if (active)
        {
            outline.SetFloat("_IsActive", 1);
            isActive = true;
        }

        else
        {
            outline.SetFloat("_IsActive", 0);
            isActive = false;
        }
    }

    public override void Interact()
    {
        if (outline.GetFloat("_IsActive") != 1)
        {
            return;
        }


        base.Interact();
    }
}
