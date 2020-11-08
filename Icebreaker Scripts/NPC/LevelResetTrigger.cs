using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResetTrigger : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == "Player")
        {
            gameManager.PlayerDied();

            GetComponent<Collider2D>().enabled = false;
        }
    }
}
