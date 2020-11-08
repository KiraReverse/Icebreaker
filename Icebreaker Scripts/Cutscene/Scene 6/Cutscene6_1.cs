using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene6_1 : BaseCutsceneController
{
    public bool isConseq = false;

    public GameObject partner;
    public Transform page;
    public float offset;

    protected override IEnumerator Cutscene()
    {
        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;

        partner.GetComponent<PartnerController>().enabled = false;

        gameManager.StartFade(1f, 1f);
        yield return new WaitForSeconds(1f);

        Vector2 temp = page.position;
        temp.x += offset;
        temp.y -= offset / 2;
        player.transform.position = temp;
        player.GetComponent<PlayerController>().SetFacingDir(Enums.Directions.left);

        temp = page.position;
        temp.x -= offset;
        temp.y -= offset / 2;
        partner.transform.position = temp;
        player.GetComponent<PlayerController>().SetPartnerFacingDir(Enums.Directions.right);

        gameManager.StartFade(0f, 1f);
        yield return new WaitForSeconds(1f);

        if (!isConseq)
        {
            partner.GetComponent<PartnerController>().enabled = true;
        }

        cutsceneManager.cutsceneOngoing = false;
        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }


}
