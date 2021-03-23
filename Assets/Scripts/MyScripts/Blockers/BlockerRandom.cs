using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerRandom : MonoBehaviour
{
    public static BlockerRandom instance;
    public GameObject[] block;
    public GameObject[] aiCar;
    public GameObject coins;
    public GameObject oil;
    public float blockRoad_range;
    public float distanceFromBlock = 20;
    public int offsetDistance = 50;
    public Transform parent;

    public Transform lastBlokPosition;
    static int blockNum = 0;
    int randomSeed = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(CoinsRandomer());
        StartCoroutine(UpdateBlock());

    }
    public IEnumerator UpdateBlock()
    {
        yield return new WaitForSeconds(1f * Time.deltaTime);

        
        while (true)
        {
            float car_z = GameManager.instance.mainPlayerInstance.transform.position.z;
            if (lastBlokPosition.position.z - car_z < distanceFromBlock)
            {
                Vector3 br = new Vector3(MyRandom(-blockRoad_range, blockRoad_range),
                    .3f, lastBlokPosition.position.z + distanceFromBlock + Random.Range(10, 20));
                //GameObject.Instantiate(block[MyRandom(0, block.Length)], br, Quaternion.Euler(270, 0, transform.localRotation.z), parent);
                //lastBlokPosition = Instantiate(block[MyRandom(0, block.Length)], br, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z), parent).transform.position;
                lastBlokPosition = Instantiate(block[MyRandom(0, block.Length)], br,
                    Quaternion.Euler(transform.localRotation.x, transform.localRotation.y,
                    transform.localRotation.z), parent).transform;
                blockNum += 1;
            }
            yield return null;
        }
       
    }public IEnumerator AICar()
    {
        yield return new WaitForSeconds(0.5f * Time.deltaTime);

        while (true)
        {
            float car_z = GameManager.instance.mainPlayerInstance.transform.position.z;
            if (lastBlokPosition.position.z - car_z < distanceFromBlock)
            {
                Vector3 br = new Vector3(MyRandom(-blockRoad_range, blockRoad_range),
                    .3f, lastBlokPosition.position.z + distanceFromBlock + Random.Range(10, 20));
                lastBlokPosition = Instantiate(aiCar[MyRandom(0, aiCar.Length)], br,
                    Quaternion.Euler(0, 0,
                    0), parent).transform;
            }
            yield return null;
        }
       
    }
    float MyRandom(float min, float max)
    {
        randomSeed++;
        Random.InitState(randomSeed +=(int) System.DateTime.Now.Millisecond);
        
        return Random.Range(min, max);
    }
    int MyRandom(int min, int max)
    {
        randomSeed++;
        Random.InitState(randomSeed += (int)System.DateTime.Now.Millisecond);
    
        return Random.Range(min, max);
    }
    IEnumerator CoinsRandomer()
    {
        while (true)
        {
            float car_z = GameManager.instance.mainPlayerInstance.transform.position.z;
            if (blockNum > 2) {
                print("Hey");
                Vector3 br = new Vector3(MyRandom(-blockRoad_range, blockRoad_range), 1f, car_z + distanceFromBlock);
                GameObject.Instantiate(coins, br, Quaternion.Euler(0, 180, 0), parent);
                
                blockNum = 0;
            }
            if (GameManager.instance.oilLvl < 90)
            {
                if (blockNum > 1)
                {
                    print("Hey");
                    Vector3 br = new Vector3(MyRandom(-blockRoad_range, blockRoad_range), 1f, car_z + distanceFromBlock);
                    GameObject.Instantiate(oil, br, Quaternion.Euler(270, 0, 0), parent);

                    blockNum = 0;
                }
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(CoinsRandomer());
        StartCoroutine(UpdateBlock());
        StartCoroutine(AICar());
    }
}
