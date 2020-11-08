using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip defaultClip;
    public AudioClip emotionClip;

    public float defaultVolume = 0.5f;
    public float emotionVolume = 0.5f;

    GameManager gameManager;
    AudioSource sfx;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        sfx = GetComponent<AudioSource>();


        if (gameManager.playerConfig.gameMode == Enums.GameMode.emotionNegative)
        {
            sfx.volume = defaultVolume;
        }

        else
        {
            sfx.volume = emotionVolume;
        }
        

        SetBGM();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            sfx.volume += 0.1f;

            if(sfx.volume >= 1.0f)
            {
                sfx.volume = 1.0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.Minus))
        {
            sfx.volume -= 0.1f;

            if (sfx.volume <= 0f)
            {
                sfx.volume = 0f;
            }
        }
    }

    void SetBGM()
    {
        if(gameManager.playerConfig.gameMode == Enums.GameMode.emotionNegative)
        {
            sfx.clip = emotionClip;
            sfx.loop = true;
            sfx.Play();
        }

        else
        {
            sfx.clip = defaultClip;
            sfx.loop = true;
            sfx.Play();
        }
    }
}
