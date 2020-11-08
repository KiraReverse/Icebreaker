using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1dot2_3 : BaseCutsceneController
{
    public Transform zork;
    public CinemachineVirtualCamera vCam;
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

            gameManager.StartFade(1f, 1f);
            yield return new WaitForSeconds(1f);
            zork.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            zorkAnim.enabled = true;
            vCam.Follow = player.transform;
            zorkAnim.Play("PC_Idle_Back");

            Vector2 tempPos = zorkSavedPos.position;
            tempPos.x -= savedPosOffset;

            player.transform.position = tempPos;

            tempPos = zorkSavedPos.position;
            tempPos.x += savedPosOffset;

            zork.position = tempPos;

            gameManager.StartFade(0f, 1f);
            yield return new WaitForSeconds(1.5f);


            cutsceneManager.cutsceneOngoing = false;
            gameManager.GameState = Enums.GameState.playing;
            gameObject.SetActive(false);
        }

        else
        {
            cutsceneManager.cutsceneOngoing = true;
            gameManager.GameState = Enums.GameState.cutscene;

            ramu.SetActive(true);

            foreach (Animator a in ramuAnims)
            {
                a.SetFloat("MovespeedY", -1f);
            }

            while (Vector2.Distance(ramu.transform.position, zorkSavedPos.position) >= 0.1f)
            {
                ramu.transform.position = Vector2.MoveTowards(ramu.transform.position, zorkSavedPos.position, 5F * Time.deltaTime);
                yield return null;
            }

            gameManager.StartFade(1f, 1f);
            yield return new WaitForSeconds(1f);
            zork.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            zorkAnim.enabled = true;
            vCam.Follow = player.transform;


            zorkAnim.Play("PC_Idle_Back");

            foreach (Animator a in ramuAnims)
            {
                a.SetFloat("MovespeedY", 0f);
                a.Play("Ramu_Idle_Back");
            }
            Vector2 tempPos = zorkSavedPos.position;
            tempPos.x -= savedPosOffset;

            player.transform.position = tempPos;

            tempPos = zorkSavedPos.position;
            tempPos.x += savedPosOffset;
            tempPos.y += savedPosOffset;

            zork.position = tempPos;


            tempPos = zorkSavedPos.position;
            tempPos.x += savedPosOffset;
            tempPos.y -= savedPosOffset;

            ramu.transform.position = tempPos;

            tempPos = zorkSavedPos.position;
            tempPos.x -= 2f;

            player.transform.position = tempPos;

            player.GetComponent<PlayerController>().SetFacingDir(Enums.Directions.right);

            gameManager.StartFade(0f, 1f);
            yield return new WaitForSeconds(1.5f);


           


            cutsceneManager.cutsceneOngoing = false;

            gameManager.GameState = Enums.GameState.playing;
            gameObject.SetActive(false);
        }


    }

 
}
