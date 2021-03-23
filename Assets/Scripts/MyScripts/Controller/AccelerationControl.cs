using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationControl : MonoBehaviour
{
    public static AccelerationControl instance;

    public Slider speed;

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(instance);
    }

    public void AcclererationController()
    {
        PlayerPrefs.SetFloat("acceleration", speed.value);
    }
}
