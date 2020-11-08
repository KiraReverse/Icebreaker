using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice Config", menuName = "Config/ChoiceConfig")]
[Serializable]
public class ChoiceConfig : ScriptableObject
{
    public List<Choices> choices = new List<Choices>();
    public List<SceneCounter> sceneCounters = new List<SceneCounter>();
    public string gameMode;

    public void EmptyChoices()
    {
        choices = new List<Choices>();
        sceneCounters = new List<SceneCounter>();
        gameMode = "";
    }

    public void SetChoice(int choiceNum, string choice, string timeTaken)
    {
        Debug.Log("choicenum "+choiceNum);
        Debug.Log("choices count " + choices.Count);
        if(choices.Count < choiceNum)
        {
            int temp = choices.Count;

            for(int i = 1; i<choiceNum - temp; ++i)
            {
                choices.Add(new Choices("1", "1"));
            }

            choices.Add(new Choices(choice, timeTaken));
        }

        else
        {
            choices[choiceNum - 1] = new Choices(choice, timeTaken);
        }
    }

    public void AddSceneCounter(string sceneName, int interactCounter, int fireboltCounter, int dashCounter, float levelTime, bool died)
    {
        sceneCounters.Add(new SceneCounter(sceneName, interactCounter, fireboltCounter, dashCounter, levelTime, died));
    }
}

[System.Serializable]
public class Choices
{
    public Choices(string _choice, string _timeTaken)
    {
        choice = _choice;
        timeTaken = _timeTaken;
    }

    public string choice;
    public string timeTaken;
}

[System.Serializable]
public class SceneCounter
{
    public SceneCounter(string _sceneName, int _interactCounter, int _fireboltCounter, int _dashCounter, float _levelTime, bool _died)
    {
        sceneName = _sceneName;
        interactCounter = _interactCounter;
        fireboltCounter = _fireboltCounter;
        dashCounter = _dashCounter;
        levelTime = _levelTime;
        died = _died;
    }
    public string sceneName;
    public int interactCounter;
    public int fireboltCounter;
    public int dashCounter;
    public float levelTime;
    public bool died;
}
