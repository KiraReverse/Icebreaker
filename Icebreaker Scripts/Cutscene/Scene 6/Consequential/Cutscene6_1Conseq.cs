using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene6_1Conseq : BaseCutsceneController
{
    public Animator partnerAnim;
    public GameObject iceRock;
    public float dropSpeed;
    public Transform rockEndWP;
    CinemachineImpulseSource impulse;

    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
    }


    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;

        impulse.GenerateImpulse();
        yield return null;


        while(Vector2.Distance(iceRock.transform.position, rockEndWP.position)> 0.001f)
        {
            iceRock.transform.position = Vector2.MoveTowards(iceRock.transform.position, rockEndWP.position, dropSpeed*Time.deltaTime);

            yield return null;
        }

        partnerAnim.SetBool("isDead", true);


        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }

 

}
