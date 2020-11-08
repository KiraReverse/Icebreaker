using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene7_2 : BaseCutsceneController
{
    CinemachineImpulseSource impulse;

    // Start is called before the first frame update
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

        cutsceneManager.cutsceneOngoing = false;

        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }
}
