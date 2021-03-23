using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    float metr = 0;

    public GameObject distanceStart;
    
    void Offseter()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Translate(0, 0, -300, Space.World);
        }

    }
    private void FixedUpdate()
    {
        GameManager.instance.score = GameManager.instance.mainPlayerInstance.transform.position.z - distanceStart.transform.position.z;
        //metr += 1 * Time.fixedDeltaTime;
        if (GameManager.instance.mainPlayerInstance.transform.position.z > 300)
        {
            Offseter();
           
            //metr = 0;
        }
        //print(metr);
    }
}
