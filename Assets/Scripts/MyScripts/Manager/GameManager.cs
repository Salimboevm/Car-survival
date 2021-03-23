using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [HideInInspector]
    public int coins;
    [HideInInspector]
    public float score;
    [HideInInspector]
    public string playerName;

    GameObject mainPlayer;

    public AudioSource sfx;
    public AudioClip sfxClip;
    public AudioClip clip;

    public GameObject loadingScreen;
    public GameObject loadObject;

    [HideInInspector]
    public GameObject mainPlayerInstance;

    public GameObject settings, home;

    public AudioSource source;

    //oil lvl
    public float oilLvl = 100;// variable to check oil level while driving
    UIManager uiManager;
    string filePath;

    //audio
    //[HideInInspector]
    //public float musicVolume = 0f;
    //[HideInInspector]
    //public float sfxVolume = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
        DontDestroyOnLoad(instance);
        DontDestroyOnLoad(sfx);
        coins = PlayerPrefs.GetInt("Coins", 0);
        SaveSystem.Load();
    }

    public IEnumerator StartGame()
    {
        
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1f;
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        PlayerPrefs.GetFloat("acceleration", 45);
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void SceneLoaded(Scene s, LoadSceneMode l)
    {
        if(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Garage")
        {
            source.Stop();
        }
        else
        {
            if(!source.isPlaying)
                source.Play();
        }
    }
    private void Update()
    {
        int time = 0;

        if (time < 3)
        {
            time += 1;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
            SaveSystem.Player(this);
        }
    }

    public void Clear()
    {
        SaveSystem.ClearCache();
    }

    public void ToSettings()
    {
        sfx.PlayOneShot(sfxClip);
        home.SetActive(false);
        settings.SetActive(true);
    }
    public void ToHome()
    {
        sfx.PlayOneShot(sfxClip);
        home.SetActive(true);
        settings.SetActive(false);
        PlayerName.instance.mainName.text = playerName;
        PlayerPrefs.SetFloat("acceleration", AccelerationControl.instance.speed.value);
        //SceneManager.LoadScene(0);
    }
    int minSceneid = 1;
    int maxSceneid = 4;
    public void StartDriving()
    {   
        sfx.PlayOneShot(sfxClip);
        int index = MyRandom(minSceneid, maxSceneid);
        mainPlayer = LoadManager.mainPlayer;
        PlayerPrefs.GetString(LoadManager.instance.name, mainPlayer.ToString());
        StartCoroutine(LoadAsynchrounosly(index));
    }
    public void StopDriving()
    {
        int index = 0;
        //AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");
        SaveSystem.Player(this);
        SceneManager.LoadScene(index);
        sfx.PlayOneShot(sfxClip);
    }
    public void Garage()
    {
        sfx.PlayOneShot(sfxClip);
        //AsyncOperation operation = SceneManager.LoadSceneAsync("Garage");
        StartCoroutine(LoadAsynchrounosly(4));
    }
    int randomSeed = 0;
    int MyRandom(int min, int max)
    {
        randomSeed++;
        Random.InitState(randomSeed += (int)System.DateTime.Now.Millisecond);

        return Random.Range(min, max);
    }

    IEnumerator LoadAsynchrounosly(int sceneid)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneid);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadObject.transform.Rotate(0,0,10);

            yield return null;
        }
    }
    private void OnEnable()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        SaveSystem.Load();
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
        SaveSystem.Player(this);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
        SaveSystem.Player(this);
    }
}
