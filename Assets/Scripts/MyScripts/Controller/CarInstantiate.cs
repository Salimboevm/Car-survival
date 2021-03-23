using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInstantiate : MonoBehaviour
{
    public float carRoad;
    public float distanceFromCar;
    [HideInInspector]
    public GameObject car;

    public Transform parent;
    private void Awake()
    {
        GameManager.instance.mainPlayerInstance = Instantiate(LoadManager.mainPlayer, parent);
    }
   
}
