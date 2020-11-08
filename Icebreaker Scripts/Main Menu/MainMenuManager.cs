using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enums;

public class MainMenuManager : MonoBehaviour
{
    public InputField nameField;
    public Text gameModeText;

    public GameObject modeSwitch;

    public PlayerConfig playerConfig;
    public ChoiceConfig choiceConfig;
    public SceneConfig sceneConfig;

    private void Awake()
    {
        choiceConfig.EmptyChoices();

        switch (playerConfig.gameMode)
        {
            default:
            case GameMode.emotionPositive:
                gameModeText.text = "Game Mode: Emotion Positive";
                break;
            case GameMode.emotionNegative:
                gameModeText.text = "Game Mode: Emotion Negative";
                break;
            case GameMode.consequencesPositive:
                gameModeText.text = "Game Mode: Inconsequential";
                break;
            case GameMode.consequencesNegative:
                gameModeText.text = "Game Mode: Consequential";
                break;
            case GameMode.agencyPositive:
                gameModeText.text = "Game Mode: Agency Not Violated";
                break;
            case GameMode.agencyNegative:
                gameModeText.text = "Game Mode: Agency Violated";
                break;

        }
    }

    public void SetPlayerType(int typeNum)
    {
        switch(typeNum)
        {
            default:
            case 1:
                playerConfig.playerType = PlayerType.PC1;
                break;
            case 2:
                playerConfig.playerType = PlayerType.PC2;
                break;
            case 3:
                playerConfig.playerType = PlayerType.PC3;
                break;
               
        }
        
    }

    public void ConfigGameMode()
    {
        switch (playerConfig.gameMode)
        {
            default:
            case GameMode.emotionPositive:
                choiceConfig.gameMode = "Emotion Positive";
                break;
            case GameMode.emotionNegative:
                choiceConfig.gameMode = "Emotion Negative";
                break;
            case GameMode.consequencesPositive:
                choiceConfig.gameMode = "Inconsequential";
                break;
            case GameMode.consequencesNegative:
                choiceConfig.gameMode = "Consequential";
                break;
            case GameMode.agencyPositive:
                choiceConfig.gameMode = "Agency Not Violated";
                break;
            case GameMode.agencyNegative:
                choiceConfig.gameMode = "Agency Violated";
                break;

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            modeSwitch.SetActive(!modeSwitch.activeSelf);
        }
    }

    public void SetPlayerName()
    {
        if (nameField.text != "")
        {
            playerConfig.playerName = nameField.text;
        }
    }

    public void SetGameMode(int gameMode)
    {
        switch (gameMode)
        {
            default:
            case 1:
                playerConfig.gameMode = GameMode.emotionPositive;
                gameModeText.text = "Game Mode: Emotion Positive";
                break;
            case 2:
                playerConfig.gameMode = GameMode.emotionNegative;
                gameModeText.text = "Game Mode: Emotion Negative";
                break;
            case 3:
                playerConfig.gameMode = GameMode.consequencesPositive;
                gameModeText.text = "Game Mode: Inconsequential";
                break;
            case 4:
                playerConfig.gameMode = GameMode.consequencesNegative;
                gameModeText.text = "Game Mode: Consequential";
                break;
            case 5:
                playerConfig.gameMode = GameMode.agencyPositive;
                gameModeText.text = "Game Mode: Agency Not Violated";
                break;
            case 6:
                playerConfig.gameMode = GameMode.agencyNegative;
                gameModeText.text = "Game Mode: Agency Violated";
                break;

        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneConfig.GetNextScene(playerConfig.gameMode, SceneManager.GetActiveScene().buildIndex));
    }

}
