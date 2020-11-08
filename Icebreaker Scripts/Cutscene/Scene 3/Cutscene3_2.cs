using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3_2 : BaseCutsceneController
{
    public Transform zork;
    public Transform zorkEndpoint;

    public Animator zorkAnim;

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;

        zork.gameObject.SetActive(true);
        zorkAnim.SetFloat("MovespeedY", 1);

        while (Vector2.Distance(zork.position, zorkEndpoint.position) > 0.05f)
        {
            zork.position = Vector2.MoveTowards(zork.position, zorkEndpoint.position, 20f * Time.deltaTime);
            yield return null;
        }

        zorkAnim.SetFloat("MovespeedY", 0);
        zorkAnim.Play("PC_Idle_Front");


        cutsceneManager.cutsceneOngoing = false;
        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }

 
}
