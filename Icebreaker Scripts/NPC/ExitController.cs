using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitController : BaseInteractableController
{
    public bool walkover;
    public float fadeTime;
    public SceneConfig sceneConfig;
    Image fadeImage;
    GameManager gameManager;

    protected override void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        fadeImage = GameObject.FindGameObjectWithTag("FadeScreen").GetComponent<Image>();

        if(walkover)
        {
            GetComponent<Collider2D>().isTrigger = true;

            isActive = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (walkover && isActive)
        {
            if (collision.tag == "Player" && collision.isTrigger)
            {
                player.GetComponent<PlayerController>().enabled = false;
                StartCoroutine(ExitFade(fadeImage, 1.0f, fadeTime));
                gameManager.LogSceneData(false);
            }
        }
        
    }

    public override void Interact()
    {
        if (walkover || !isActive )
        {
            return;
        }

        player.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(ExitFade(fadeImage, 1.0f, fadeTime));
        gameManager.LogSceneData(false);
        
    }

    IEnumerator ExitFade(Image img, float to, float time)
    {

        float from = img.color.a;
        float timer = 0;

        while (timer < time)
        {

            from = Mathf.Lerp(from, to, timer);
            img.color = new Color(img.color.r, img.color.g, img.color.b, from);
            timer += Time.deltaTime;

            yield return null;
        }


        SceneManager.LoadScene(sceneConfig.GetNextScene(gameManager.playerConfig.gameMode, SceneManager.GetActiveScene().buildIndex));
    }
}
