using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpikeTrigger : MonoBehaviour
{
    public Enums.Directions launchDirection;
    public int numberOfSpikes;

    SpikeManager spikeManager;
    // Start is called before the first frame update
    void Start()
    {
        spikeManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpikeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == "Player")
        {
            spikeManager.LaunchSpikesFrom(launchDirection, numberOfSpikes);
            gameObject.SetActive(false);
        }
    }
}
