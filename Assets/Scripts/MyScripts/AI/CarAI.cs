using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfoAI
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;

}
public class CarAI : MonoBehaviour
{
    [Header("Move")]
    public List<AxleInfoAI> axleInfos; // the information about each individual axle
    public float maxMotorTorque = 0; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle = 30; // maximum steer angle the wheel can have
    public float steering; // variable to use steering car`s wheels
    public bool canGo = true;
    
    
    public enum ConMode { c1, c2, c3 }
    public ConMode conMode;
    
    [Header("Physics")]
    public float vel;
    public AudioSource source;
    public Rigidbody rb;

    [Header("Sensors")]
    public float sensorLength = 5f;
    public Vector3 backSensorPos = new Vector3(0, 0f, 0f);
    public float sideSensorPos = 0.2f;
    public float angleSensor = 30f;

    private bool blockWay = false;//when it is true starts blocking way
    float turnMultiplier = 0;//turns by detecting car 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    public IEnumerator MyFixedUpdate()
    {
        float motor = 0;
        while (true)
        {
            if (canGo) // if cango and oil level is more than 0 car can move
            {
                do
                {
                    motor += rb.velocity.z + maxMotorTorque * Time.fixedDeltaTime;

                } while (motor == maxMotorTorque);

                foreach (AxleInfoAI axleInfo in axleInfos)
                {
                    if (axleInfo.steering)
                    {
                        if (blockWay)
                        {
                            axleInfo.leftWheel.steerAngle = maxSteeringAngle * turnMultiplier;
                            axleInfo.rightWheel.steerAngle = maxSteeringAngle * turnMultiplier;
                        }
                    }
                    if (axleInfo.motor)
                    {
                        axleInfo.leftWheel.motorTorque = motor;
                        axleInfo.rightWheel.motorTorque = motor;
                    }
                    switch (conMode)
                    {
                        case ConMode.c1:
                            {
                                WheelSimulation(axleInfo.leftWheel);
                                WheelSimulation(axleInfo.rightWheel);
                            }
                            break;

                    }
                }
                yield return null;
            }
        }

    }
    void WheelSimulation(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform;

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    void WheelSimulation(Material wheel)
    {

        float offset = Input.GetAxis("Vertical") / 10;

        wheel.mainTextureOffset += new Vector2(0, offset);
    }

    void Sensors()
    {
        RaycastHit hit;
        Vector3 startPos = transform.position + backSensorPos;
        startPos += transform.forward * backSensorPos.z;
        startPos += transform.up * backSensorPos.y;

        //float turnMultiplier = 0;//turns by detecting car 
        blockWay = false;

        //front center
        if(Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(startPos, hit.point);
        }

        //front right
        startPos += transform.right * sideSensorPos;
        if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.gameObject.layer == 13)
            {
                Debug.DrawLine(startPos, hit.point);
                blockWay = true;
                turnMultiplier -= 1f;
            }
        }

        //front right angle
        else if (Physics.Raycast(startPos, Quaternion.AngleAxis(angleSensor, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.gameObject.layer == 13)
            {
                Debug.DrawLine(startPos, hit.point);
                blockWay = true;
                turnMultiplier -= 0.5f;
            }
        }

        //front left
        startPos -= transform.right * sideSensorPos * 2;
        if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.gameObject.layer == 13)
            {
                Debug.DrawLine(startPos, hit.point);
                blockWay = true;
                turnMultiplier += 1f;
            }
        }
        
        //front left angle
        else if (Physics.Raycast(startPos, Quaternion.AngleAxis(-angleSensor, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.gameObject.layer == 13)
            {
                Debug.DrawLine(startPos, hit.point);
                blockWay = true;
                turnMultiplier += 0.5f;
            }
        }
    }

    void OnEnable()
    {
        StartCoroutine("MyFixedUpdate");
    }
    void OnDisable()
    {
        StopCoroutine("MyFixedUpdate");
    }
}


