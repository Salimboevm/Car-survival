using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool breaking;
    public bool steering;

}

//try with physics velocity moving car
public class BaseController : MonoBehaviour
{
    public static BaseController instance;

    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque = 0; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle = 30; // maximum steer angle the wheel can have
    public float maxBreakTorque = 0; // variable to start breaking the car

    public bool canGo = true;
    bool isBreaking = false;

    public static float horizontal = 0;
    public enum ConMode { c1, c2, c3 }
    public ConMode conMode;

    [HideInInspector]
    public float score;
    [HideInInspector]
    public float coins;

    public float vel;
    public AudioSource source;

    UIManager uiManager;
    public Rigidbody rb;


    public float steering; // variable to use steering car`s wheels
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        PlayerPrefs.GetFloat("acceleration", 50);
        source.Play();
        StartCoroutine(GameManager.instance.StartGame());
    }

    public IEnumerator MyFixedUpdate()
    {
        score += 1f * Time.fixedDeltaTime;
        //GameManager.instance.score = score;
        if (score > 100)
        {
            score += 1.5f * Time.fixedDeltaTime;
            //GameManager.instance.score = score;
        }
        float motor = 0;
        while (true)
        {
            if (canGo) // if cango and oil level is more than 0 car can move
            {
                if (GameManager.instance.oilLvl <= 0)
                    isBreaking = true;
                if (GameManager.instance.oilLvl > 0)
                {
                    if (isBreaking == false)
                    {
                        GameManager.instance.oilLvl -= 0.5f * Time.deltaTime; // decrease oil level by real time muultipled by half
                        //float motorMultiplier = Input.GetAxis("Vertical");
                        //              motor = maxMotorTorque * motorMultiplier;

                        float steering = maxSteeringAngle * 10 * SimpleInput.GetAxis("Horizontal") * Time.fixedDeltaTime;

                        do
                        {
                            motor += rb.velocity.z + maxMotorTorque * Time.fixedDeltaTime;

                        } while (motor == maxMotorTorque);

                        foreach (AxleInfo axleInfo in axleInfos)
                        {
                            if (axleInfo.steering)
                            {
                                axleInfo.leftWheel.steerAngle = steering;
                                axleInfo.rightWheel.steerAngle = steering;
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

                    }
                }
                else
                {

                    maxBreakTorque -= rb.velocity.z - maxMotorTorque / Time.deltaTime; // variable to start breaking the car
                    print("vel: " + rb.velocity);
                    foreach (AxleInfo axleInfo in axleInfos)
                    {
                        if (axleInfo.steering)
                        {
                            axleInfo.leftWheel.steerAngle = steering;
                            axleInfo.rightWheel.steerAngle = steering;
                        }
                        if (axleInfo.breaking)
                        {
                            axleInfo.leftWheel.brakeTorque = maxBreakTorque;
                            axleInfo.rightWheel.brakeTorque = maxBreakTorque;
                            print(axleInfo.leftWheel.brakeTorque);
                            if (rb.velocity.z <= 0 && GameManager.instance.oilLvl <= 0)
                            {
                                Time.timeScale = 0;
                                uiManager.gameOverPanel.SetActive(true);
                                isBreaking = false;
                            }
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

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 8)
        {
            GameManager.instance.sfx.PlayOneShot(GameManager.instance.clip);
            Destroy(collision.gameObject);
            Time.timeScale = 0f;
            uiManager.gameOverPanel.SetActive(true);
            source.Pause();
            BlockerRandom.instance.StopCoroutine(BlockerRandom.instance.UpdateBlock());
        }
        if (collision.gameObject.layer == 10)
        {
            GameManager.instance.sfx.PlayOneShot(GameManager.instance.clip);
            Destroy(collision.gameObject);
            Time.timeScale = 0f;
            uiManager.gameOverPanel.SetActive(true);
            source.Pause();
            BlockerRandom.instance.StopCoroutine(BlockerRandom.instance.UpdateBlock());
        }
        if (collision.gameObject.layer == 11)
        {
            GameManager.instance.sfx.PlayOneShot(GameManager.instance.clip);
            Time.timeScale = 0f;
            uiManager.gameOverPanel.SetActive(true);
            source.Pause();
            BlockerRandom.instance.StopCoroutine(BlockerRandom.instance.UpdateBlock());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            coins++;
            GameManager.instance.coins += 1;


            uiManager.UpdateCoin((int)coins);
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 12)// check to layer of gameobject oil
        {
            //if true add to oil lvl 
            //update ui
            //destroy game object
            GameManager.instance.oilLvl += 0.5f;

            Destroy(other.gameObject);
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
