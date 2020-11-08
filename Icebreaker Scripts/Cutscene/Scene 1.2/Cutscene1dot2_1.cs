using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1dot2_1 : BaseCutsceneController
{
    public Transform zork;
    public Animator zorkAnim;
    public Transform zorkExit;

    private void Start()
    {
        zorkAnim.Play("PC_Idle_Back");
    }

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;
     

        while (Vector2.Distance(zork.position, zorkExit.position) > 0.1f)
        {
            zorkAnim.SetFloat("MovespeedY", -1);
            zork.position = Vector2.Lerp(zork.position, zorkExit.position, 3f * Time.deltaTime);

            yield return null;
        }

        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }
}
