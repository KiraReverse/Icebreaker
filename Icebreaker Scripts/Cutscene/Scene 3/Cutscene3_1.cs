using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3_1 : BaseCutsceneController
{
    public SpriteRenderer grassRender;
    
    protected override IEnumerator Cutscene()
    {

        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;

        float temp = 1;
        while(grassRender.color.a>=0f)
        {
            temp -= 0.005f;
            grassRender.color = new Color(grassRender.color.r, grassRender.color.g, grassRender.color.b, temp);
            yield return null;
        }


        cutsceneManager.cutsceneOngoing = false;
        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }
}
