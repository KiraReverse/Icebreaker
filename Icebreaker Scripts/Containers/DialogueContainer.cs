using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;


[CreateAssetMenu(fileName = "New Dialogue Container", menuName = "Container/Dialogue Container")]
public class DialogueContainer : BaseContainer
{
    public List<DialogueLine> dialogueLines;
}

[System.Serializable]
public class DialogueLine
{
#pragma warning disable 0649
    [Tooltip("Set speaker here.")]
    [SerializeField] private Speakers speaker;
    public Speakers Speaker { get => speaker; }
    [Tooltip("Check this box if the following dialogue is trait dependant. If traitSpecific is not checked, it defaults to dialogue1")]
    [SerializeField] private bool modeSpecific = false;

    [Tooltip("EmotionPositive")]
    [SerializeField] private string field1;
    [Tooltip("EmotionNegative")]
    [SerializeField] private string field2;
    [Tooltip("Inconsequenctial")]
    [SerializeField] private string field3;
    [Tooltip("Consequential")]
    [SerializeField] private string field4;
    [Tooltip("Agency Not Violated")]
    [SerializeField] private string field5;
    [Tooltip("Agency Violated")]
    [SerializeField] private string field6;
#pragma warning restore 0649

    public string GetDialogueLine(GameMode trait = GameMode.emotionPositive)
    {
        if (modeSpecific)
        {
            switch (trait)
            {
                default:
                case GameMode.emotionPositive:
                    return field1;

                case GameMode.emotionNegative:
                    return field2;

                case GameMode.consequencesPositive:
                    return field3;

                case GameMode.consequencesNegative:
                    return field4;

                case GameMode.agencyPositive:
                    return field5;

                case GameMode.agencyNegative:
                    return field6;
            }
        }

        else
        {
            return field1;
        }

    }

    public string GetChoiceOne()
    {
        return field2;
    }

    public string GetChoiceTwo()
    {
        return field3;
    }
}


