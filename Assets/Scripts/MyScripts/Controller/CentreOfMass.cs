using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// calss for initialising center of mass
/// </summary>
public class CentreOfMass : MonoBehaviour
{
    public Vector3 centerOfMass;//to initialise in game
    public bool awake; // to turn on center of mass

    private void Update()
    {
        BaseController.instance.rb.centerOfMass = centerOfMass;
        BaseController.instance.rb.WakeUp();
        awake = !BaseController.instance.rb.IsSleeping();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 0.2f);
    }
}
