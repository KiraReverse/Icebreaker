using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene7_1Consequential : BaseCutsceneController
{
    public int choiceNum = 7;
    public Transform zorkStartWp;


    public float yOffset;
    public float moveSpeed;

    public GameObject zork;
    public Animator partnerAnim;
    public PartnerController partnerCon;

    Vector2 zorkEndWp;

    void Start()
    {

    }

    protected override IEnumerator Cutscene()
    {


        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;


        if (choiceManager.CheckChoice(choiceNum) != 1)
        {
            zork.SetActive(true);
            partnerCon.enabled = false;
            zork.transform.position = zorkStartWp.position;

            Vector2 temp = player.transform.position;
            temp.y -= yOffset;

            zorkEndWp = temp;

            partnerAnim.SetFloat("MovespeedY", 1f);
            

            while (Vector2.Distance(zork.transform.position, zorkEndWp)> 0.01f)
            {
                zork.transform.position = Vector2.MoveTowards(zork.transform.position, zorkEndWp, moveSpeed * Time.deltaTime);
                yield return null;
            }

            partnerAnim.SetFloat("MovespeedY", 0f);

            player.GetComponentInChildren<Animator>().Play("PC_Idle_Front");

        }

        else
        {
            Debug.Log("derp");
        }

        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }
}
