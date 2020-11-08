using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    bool hasHit = false;
    [HideInInspector] public int iceWallHitCount = 0;
    private void Start()
    {
        Destroy(gameObject, 6f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.gameObject.tag == "NPC")
        {
            if (collision.gameObject.GetComponent<IceBlockController>() != null && !hasHit)
            {
                hasHit = true;
                collision.gameObject.GetComponent<IceBlockController>().Blasted();
                Destroy(gameObject);
            }

            else if (collision.gameObject.GetComponent<ChessPieceController>() != null && !hasHit)
            {
                hasHit = true;
                collision.gameObject.GetComponent<ChessPieceController>().Blasted(this);
                Destroy(gameObject);
            }

            else if (collision.gameObject.GetComponent<IceWallController>() != null)
            {
                collision.gameObject.GetComponent<IceWallController>().Blasted(this);
                iceWallHitCount++;

                if(iceWallHitCount > 1)
                {
                    Destroy(gameObject);
                }
            }
            
        }


        else if (collision.gameObject.tag != "Fireball" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
