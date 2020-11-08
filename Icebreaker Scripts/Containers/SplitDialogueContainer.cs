using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Split Dialogue Container", menuName = "Container/SplitDialogueContainer")]
public class SplitDialogueContainer : BaseContainer
{
    public int choiceNumber;

    public DialogueContainer result1;
    public DialogueContainer result2;
}
