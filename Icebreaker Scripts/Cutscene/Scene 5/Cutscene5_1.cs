using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene5_1 : BaseCutsceneController
{
    public GameObject partner;
    public PartnerController partnerCon;

    public Transform fireballSpawn;
    public GameObject fireballPrefab;

    protected override IEnumerator Cutscene()
    {

        cutsceneManager.cutsceneOngoing = true;
        gameManager.GameState = Enums.GameState.cutscene;


        partnerCon.enabled = false;

        GameObject fireball = Instantiate(fireballPrefab, fireballSpawn.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200f);


        yield return new WaitForSeconds(3f);

        partnerCon.enabled = true;
        cutsceneManager.cutsceneOngoing = false;
        gameManager.GameState = Enums.GameState.playing;

        gameObject.SetActive(false);
    }

 
}
