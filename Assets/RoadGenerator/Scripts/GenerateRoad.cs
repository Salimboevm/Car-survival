using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    //public GameObject targetObj;

    public int roadLength;
    public int offsetDistance=80;

    private void FixedUpdate()
    {    
        if (!GameManager.instance.mainPlayerInstance) return;

        if (GameManager.instance.mainPlayerInstance.transform.position.z - transform.position.z>offsetDistance)
        {
            transform.Translate(0,0,5* roadLength,Space.World);
        }
    }    
}
