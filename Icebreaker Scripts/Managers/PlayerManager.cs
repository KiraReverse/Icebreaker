using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject zork;
    public CinemachineVirtualCamera playerVcam;

    bool isPC = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwapCharacters()
    {
        if(isPC)
        {
            isPC = false;

            zork.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<PlayerController>().enabled = false;

            playerVcam.Follow = zork.transform;
        }

        else
        {
            zork.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<PlayerController>().enabled = true;

            playerVcam.Follow = player.transform;
        }
    }
}
