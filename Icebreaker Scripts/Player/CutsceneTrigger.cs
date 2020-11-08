using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CutsceneTrigger : MonoBehaviour
{

    public GameObject page;
    CutsceneManager cutsceneManager;
    GameManager gameManager;
    GameObject player;

    bool isTouched;

    private void Awake()
    {
        cutsceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CutsceneManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && gameManager.GameState == Enums.GameState.playing && !isTouched)
        {
            isTouched = true;

            cutsceneManager.PlayNextCutscene();
            player.GetComponent<PlayerController>().IdleAnim();
            gameObject.SetActive(false);

            if (page != null)
            {
                page.SetActive(false);
            }
        }

        else if(collision == null)
        {
            Debug.Log("broken collision");
        }
    }

}
