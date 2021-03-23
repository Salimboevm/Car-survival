using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataOfPlayer
{
    //public int coins;
    public float score;

    public DataOfPlayer(GameManager game)
    {
        //coins = game.coins;
        score = game.score;
    }
}
