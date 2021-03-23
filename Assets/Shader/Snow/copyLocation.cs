using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyLocation : MonoBehaviour{
    public Transform target;
    public bool saveOffset = true;
    Vector3 offset;
    void Start(){
        offset = transform.position - target.position;
    }

    void FixedUpdate(){
        if(saveOffset)
            transform.position = target.position + offset;
        else
            transform.position = target.position;
    }
}
