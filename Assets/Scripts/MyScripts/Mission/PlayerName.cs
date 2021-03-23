using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public static PlayerName instance;
    public Text playerName, mainName;

    //string nameObj;

    private void Awake()
    {
        instance = this;
        GameManager.instance.playerName = PlayerPrefs.GetString("PlayerName", "Player");
    }

    private void Start()
    {
        //mainName.text = nameObj;
        mainName.text = GameManager.instance.playerName;
    }
    public void Change()
    {
        GameManager.instance.playerName = playerName.text;
        PlayerPrefs.SetString("PlayerName", playerName.text);
        PlayerPrefs.Save();
    }
}
