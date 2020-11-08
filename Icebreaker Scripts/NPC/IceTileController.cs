using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTileController : MonoBehaviour
{
    bool rolling = false;
    public Transform marker;
    public float speed = 400f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !collision.isTrigger)
        {
            if (!rolling)
            {
                StartCoroutine(Roll(collision.gameObject.transform));
            }
        }
    }

    IEnumerator Roll(Transform player)
    {
        rolling = true;
        player.gameObject.GetComponent<PlayerController>().enabled = false;

        Collider2D[] temp = player.GetComponents<Collider2D>();

        foreach( Collider2D c in temp)
        {
            c.enabled = false;
        }

        while (Vector2.Distance(player.position, marker.position) >0.001f)
        {
            player.position = Vector2.MoveTowards(player.position, marker.position, speed * Time.deltaTime);
            yield return null;
        }

        player.gameObject.GetComponent<PlayerController>().enabled = true;
        foreach (Collider2D c in temp)
        {
            c.enabled = true;
        }

        rolling = false;
    }

}
