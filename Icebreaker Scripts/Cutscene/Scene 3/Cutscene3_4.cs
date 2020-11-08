using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3_4 : BaseCutsceneController
{
    public Transform zork;
    public Transform zorkOutpoint;

    public Animator zorkAnim;

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;

        zorkAnim.SetFloat("MovespeedY", 1);

        while (Vector2.Distance(zork.position, zorkOutpoint.position) > 0.05f)
        {
            zork.position = Vector2.MoveTowards(zork.position, zorkOutpoint.position, 10f * Time.deltaTime);
            yield return null;
        }

        zork.gameObject.SetActive(false);

        cutsceneManager.cutsceneOngoing = false;
        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }

}
