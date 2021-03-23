using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;
    [HideInInspector]
    public static GameObject mainPlayer;
    [HideInInspector]
    public string name;

    bool defaultValue = true;
    private void Awake()
    {
        instance = this;
    }

    public void LoadCar(string directId)
    {
        mainPlayer = (GameObject)Resources.Load(directId);
        PlayerPrefs.SetString(name, mainPlayer.ToString());
    }
}