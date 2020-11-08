using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    public enum PlayerType
    {
        PC1,
        PC2,
        PC3
    }

    public enum GameState
    {
        playing,
        paused,
        cutscene
    }

    public enum GameMode
    {
        emotionPositive, 
        emotionNegative,
        consequencesPositive,   //consequential
        consequencesNegative,   //inconsequential
        agencyPositive,         //agency not violate
        agencyNegative          //agency violated
    }

    public enum Directions
    {
        up,
        down,
        left,
        right
    }

    public enum Speakers
    {
        Player,
        Zork,
        Narrator,
        Villager,
        Ramu
    }
}
