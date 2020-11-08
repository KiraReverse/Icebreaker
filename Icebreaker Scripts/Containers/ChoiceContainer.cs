using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice Container", menuName = "Container/Choice Container")]
public class ChoiceContainer : BaseContainer
{

    public Enums.Speakers speaker;

    public int choiceNumber;

    public string question;

    public string option1;
    public string option2;

}
