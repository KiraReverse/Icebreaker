using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceOffPlayerCon : MonoBehaviour
{
    PlayerController playercon;
    // Start is called before the first frame update
    void Start()
    {
        playercon = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playercon.enabled = false;
    }
}
