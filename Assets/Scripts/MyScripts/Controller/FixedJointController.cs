using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJointController : MonoBehaviour
{
    private void Start()
    {
        var car = GameManager.instance.mainPlayerInstance;
        //GetComponent<ConfigurableJoint>().connectedBody = car.GetComponent<Rigidbody>();
    }
}
