using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageManager : MonoBehaviour
{
    public GameObject main, selection, bg;

    public void Main()
    {
        main.SetActive(true);
        bg.SetActive(false);
        selection.SetActive(false);
    }
    public void Selection()
    {
        bg.SetActive(true);
        selection.SetActive(true);
        main.SetActive(false);
    }
}
