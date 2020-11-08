using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1dot2_4 : BaseCutsceneController
{
    public Transform zork;
    public Animator zorkAnim;
    public Transform zorkSavedPos;
    public Transform zorkExit;

    public GameObject ramu;
    public Animator[] ramuAnims;

    public float savedPosOffset;

    public int choiceNum;

    protected override IEnumerator Cutscene()
    {
        if (choiceManager.CheckChoice(choiceNum) == 1)
        {

            cutsceneManager.cutsceneOngoing = true;
            gameManager.GameState = Enums.GameState.cutscene;

            zorkAnim.SetFloat("MovespeedY", 1);

            while (Vector2.Distance(zork.position, zorkExit.position) >= 0.1f)
            {
                zork.position = Vector2.MoveTowards(zork.position, zorkExit.position, 2f * Time.deltaTime);
                yield return null;
            }


            cutsceneManager.cutsceneOngoing = false;

            gameManager.GameState = Enums.GameState.playing;

            zork.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        else
        {
            cutsceneManager.cutsceneOngoing = true;
            gameManager.GameState = Enums.GameState.cutscene;

            Vector2 ramuExitPos = zorkExit.position;
            ramuExitPos.x += savedPosOffset;

            foreach (Animator a in ramuAnims)
            {
                a.Play("Ramu_Idle_Front");
            }

            foreach (Animator a in ramuAnims)
            {
                a.SetFloat("MovespeedY", 1f);
            }
            zorkAnim.SetFloat("MovespeedY", 1f);

            while (Vector2.Distance(zork.position, zorkExit.position) >= 0.1f)
            {
                zork.position = Vector2.MoveTowards(zork.position, zorkExit.position, 2f * Time.deltaTime);
                ramu.transform.position = Vector2.MoveTowards(ramu.transform.position, ramuExitPos, 2f * Time.deltaTime);
                yield return null;
            }

            cutsceneManager.cutsceneOngoing = false;

            gameManager.GameState = Enums.GameState.playing;

            zork.gameObject.SetActive(false);
            ramu.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }


    }

}
