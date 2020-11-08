using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1dot2_2 : BaseCutsceneController
{

    public Transform zork;
    public CinemachineVirtualCamera vCam;

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;

        Vector2 mid = (zork.position + player.transform.position) / 2;
        GameObject temp = new GameObject("temp");
        temp.transform.position = mid;

        vCam.Follow = temp.transform;

        while (Vector2.Distance(vCam.transform.position, temp.transform.position) > 0.1f) 
        {
            yield return null;
        }

        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }
}
   
