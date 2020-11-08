using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : BaseCutsceneController
{
    protected override IEnumerator Cutscene()
    {
        gameManager.StartFade(1f, 2f);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("LoginScreen");
    }
}
