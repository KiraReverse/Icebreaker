using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerController : MonoBehaviour
{
    public Transform player;
    public Transform partnerWaypoint;
    public PlayerController playerController;
    public SpriteRenderer playerSr;
    public SpriteRenderer partnerSr;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = partnerWaypoint.position;
    }
    private void Update()
    {
        if (gameManager.GameState == Enums.GameState.playing)
        {
            transform.position = Vector3.Lerp(transform.position, partnerWaypoint.position, 10f * Time.deltaTime);
        }

        if(player.position.y > transform.position.y)
        {
            partnerSr.sortingOrder = playerSr.sortingOrder + 1;
        }

        else if (player.position.y < transform.position.y)
        {
            partnerSr.sortingOrder = playerSr.sortingOrder - 1;
        }

        else
        {
            partnerSr.sortingOrder = playerSr.sortingOrder;
        }

    }
}

