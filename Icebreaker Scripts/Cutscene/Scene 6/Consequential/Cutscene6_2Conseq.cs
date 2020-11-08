using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene6_2Conseq : BaseCutsceneController
{
    public SpriteRenderer iceRock;
    public Animator partnerAnim;
    public PartnerController partnerCon;
    public int choiceNum;


    Material frozenMat;
    // Start is called before the first frame update
    void Start()
    {
        frozenMat = iceRock.material;
    }

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;


        if (choiceManager.CheckChoice(choiceNum) == 1)
        {
            player.GetComponent<PlayerController>().hasPartner = false;
            StartCoroutine(Melt());
            yield return new WaitForSeconds(1f);
            partnerAnim.SetBool("isDead", false);
            partnerAnim.Play("PC_Idle_Right");

            yield return null;

            partnerCon.enabled = true;

        }

        else
        {
            Debug.Log("derp");
        }

        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }



    IEnumerator Melt()
    {
        float temp = 1f;

        while (temp > 0f)
        {
            temp -= Time.deltaTime;
            frozenMat.SetFloat("_Fade", temp);
            yield return null;
        }

        iceRock.gameObject.SetActive(false);


    }

}
