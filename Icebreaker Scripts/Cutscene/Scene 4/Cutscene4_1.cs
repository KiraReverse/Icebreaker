using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene4_1 : BaseCutsceneController
{

    public Transform page;
    public CinemachineVirtualCamera vcam;
    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;


        vcam.Follow = page;

        yield return new WaitForSeconds(2f);

        vcam.Follow = player.transform;


        


        cutsceneManager.cutsceneOngoing = false;
        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }
}
