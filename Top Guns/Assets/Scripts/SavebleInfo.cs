using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class SavebleInfo
{
    public int Score;
    public int HighScore;
    public int Coins;

    public SavebleInfo(int score, int highScore, int coins)
    {
        Score += score;
        HighScore = highScore;
        Coins = coins;
    }
}
