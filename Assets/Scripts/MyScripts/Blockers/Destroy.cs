using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == 10)
        //{
        //    print("aa");
        //    Destroy(collision.gameObject);
        //}
    }
}
