using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3_3 : BaseCutsceneController
{
    public Transform zork;
    public Transform zorkEndpoint;
    public float posOffset;

    public Animator zorkAnim;

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;

        gameManager.GameState = Enums.GameState.cutscene;
        gameManager.StartFade(1f, 1f);
        yield return new WaitForSeconds(1f);

        zorkAnim.Play("PC_Idle_Left");

        Vector2 tempPos = zorkEndpoint.position;
        tempPos.x -= posOffset;

        player.transform.position = tempPos;

        tempPos = zorkEndpoint.position;
        tempPos.x += posOffset;
        tempPos.y += 0.9f;

        zork.position = tempPos;

        player.GetComponentInChildren<Animator>().SetFloat("MovespeedX", 0f);
        player.GetComponentInChildren<Animator>().SetFloat("MovespeedY", 0f);
        player.GetComponentInChildren<Animator>().Play("PC_Idle_Right");


        gameManager.StartFade(0f, 1f);
        yield return new WaitForSeconds(1f);

        

        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }


}
