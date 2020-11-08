using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Player Config", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public PlayerType playerType;
    public string playerName;
    public GameMode gameMode;

    public KeyCode interactKey = KeyCode.Space;
    public KeyCode dodgeKey = KeyCode.LeftShift;
    public KeyCode fireballKey = KeyCode.E;
    public KeyCode swapPartner = KeyCode.Q;

}
