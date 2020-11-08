using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionCountManager : MonoBehaviour
{

    GameManager gameManager;
    public int maxFireball;
    public int maxStabs;

    public Text stabText;
    public Text fireballText;

    int currFireball = 0;
    int currStabs = 0;
    bool isDead = false;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        UpdateStabCounter();
        UpdateFireballCounter();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currStabs > maxStabs || currFireball > maxFireball)
        {
            if (!isDead)
            {
                isDead = true;
                gameManager.PlayerDied();
            }
        }
    }

    public void Stabbed()
    {
        currStabs++;
        UpdateStabCounter();
    }

    public void Fireballed()
    {
        currFireball++;
        UpdateFireballCounter();
    }


    void UpdateStabCounter()
    {
        stabText.text = "Knife: " +currStabs +"/" + maxStabs;
    }

    void UpdateFireballCounter()
    {
        fireballText.text = "Firebolt: " + currFireball + "/" + maxFireball;
    }

}
