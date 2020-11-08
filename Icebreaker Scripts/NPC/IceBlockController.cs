using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockController : BaseInteractableController
{

    public GameObject unfrozenTarget;

    GameManager gameManager;




    public SpriteRenderer frozenSr;
    Material frozenMat;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        frozenMat = frozenSr.material;
        isActive = true;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        frozenSr.sprite = unfrozenTarget.GetComponentInChildren<SpriteRenderer>().sprite;
        frozenSr.transform.position = unfrozenTarget.GetComponentInChildren<SpriteRenderer>().transform.position;
        frozenSr.transform.localScale = unfrozenTarget.GetComponentInChildren<SpriteRenderer>().transform.localScale;
        frozenSr.transform.rotation = unfrozenTarget.GetComponentInChildren<SpriteRenderer>().transform.rotation;


        GetComponent<BoxCollider2D>().offset = unfrozenTarget.GetComponent<Collider2D>().offset;
        GetComponent<BoxCollider2D>().size = unfrozenTarget.GetComponent<BoxCollider2D>().size;
    }

    public override void Interact()
    {
        if(!isActive)
        {
            return;
        }

        if(!isZorkInteractable && player.GetComponent<PlayerController>().isZork)
        {
            return;
        }

        StartCoroutine(Melt());
        gameManager.ActivateNextObjective(this.gameObject);
    }

    public void Blasted()
    {
        StartCoroutine(Blast());
        gameManager.ActivateNextObjective(this.gameObject);
    }

    IEnumerator Melt()
    {
        float temp = 1f;
        isActive = false;

        if (unfrozenTarget != null)
        {
            unfrozenTarget.SetActive(true);
        }
        while (temp > 0f)
        {
            temp -= Time.deltaTime;
            frozenMat.SetFloat("_Fade", temp);
            yield return null;
        }

        gameObject.SetActive(false);

        
    }

    IEnumerator Blast()
    {
        float temp = 1f;
        isActive = false;

        if (unfrozenTarget != null)
        {
            unfrozenTarget.SetActive(false);
        }
        while (temp > 0f)
        {
            temp -= Time.deltaTime;
            frozenMat.SetFloat("_Fade", temp);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
