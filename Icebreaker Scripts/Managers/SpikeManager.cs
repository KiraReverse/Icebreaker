using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class SpikeManager : MonoBehaviour
{
    [Header("Spike Properties", order = 0)]
    [Tooltip("Speed of the spike")]
    public float spikeSpeed;

    [Tooltip("Distance away from player that spike spawns")]
     Vector2 spikeOffset;

    [Tooltip("Delay range between spike launches, x = min delay, y = max delay")]
    public Vector2 spikeDelay;

    [Header(" Object References", order = 1)]
    public GameObject spikePrefab;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        spikeOffset.y = Camera.main.orthographicSize + 1;
        spikeOffset.x = ((spikeOffset.y * Camera.main.aspect) / 2) +1; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchSpikesFrom(Directions dir, int numOfSpikes)
    {
        StartCoroutine(LaunchSpikes(dir, numOfSpikes));
    }

    void LaunchSpike(Directions dir)
    {
        Vector3 spikePos = player.transform.position;

        float variance = Random.Range(-5, 5);

        switch(dir)
        {
            case Directions.up:
                spikePos.y += spikeOffset.y;
                spikePos.x += variance;

                break;

            case Directions.down:
                spikePos.y -= spikeOffset.y;
                spikePos.x += variance;
                break;

            case Directions.left:
                spikePos.x -= spikeOffset.x;
                spikePos.y += variance;
                break;

            case Directions.right :
                spikePos.x += spikeOffset.x;
                spikePos.y += variance;
                break;
        }

        Vector3 spikeDir = player.transform.position - spikePos;
        float angle = Mathf.Atan2(spikeDir.y, spikeDir.x) * Mathf.Rad2Deg -90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
       
        GameObject spike = Instantiate(spikePrefab, spikePos, Quaternion.identity);
        spike.transform.rotation = q;

        spike.GetComponent<Rigidbody2D>().AddForce(spike.transform.up * spikeSpeed);
    }

 
    IEnumerator LaunchSpikes(Directions dir, int numOfSpikes)
    {
        for (int i = 0; i < numOfSpikes; ++i)
        {
            LaunchSpike(dir);
            float delay = Random.Range(spikeDelay.x, spikeDelay.y);
            yield return new WaitForSeconds(delay);
        }
    }

}
