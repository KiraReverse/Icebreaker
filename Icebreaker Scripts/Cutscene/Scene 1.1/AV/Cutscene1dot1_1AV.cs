using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1dot1_1AV : BaseCutsceneController
{
    public List<Transform> waypoints;
    public GameObject objective;

    public float moveSpeed;

    public Animator pAnim;

    public GameObject diagUI;
    protected override IEnumerator Cutscene()
    {
        int wpCounter = 0;

        pAnim.SetFloat("MovespeedY", 1f);
        pAnim.SetFloat("MovespeedX", 0f);

        while (Vector2.Distance(player.transform.position, waypoints[wpCounter].position) > 0.05f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, waypoints[wpCounter].position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        pAnim.SetFloat("MovespeedY", 0f);
        pAnim.Play("PC_Idle_Back");

        objective.GetComponent<BaseInteractableController>().Interact();

        yield return new WaitForSeconds(3f);

        yield return new WaitWhile(() => diagUI.activeSelf);

        cutsceneManager.PlayNextCutscene();
    }
}
