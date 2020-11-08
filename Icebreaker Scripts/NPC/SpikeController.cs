using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpikeController : MonoBehaviour
{
    public float lifetime;
    public float impactMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity* impactMultiplier);
            Destroy(gameObject);
        }

    }
}
