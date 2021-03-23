using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text coinScoreText;
    public GameObject gameOverPanel;
    public GameObject adsButton;
    public GameObject restart;
    public AudioSource audio;
    AudioManager audioManager;
    //FPS
    public Text fps;
    float fpsC;
    //End FPS
    //slider for ui oil
    public Slider oilLvl;
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    int fpsCount = 0;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Update()
    {
        fpsC += (1 / Time.deltaTime);
        fpsCount++;
        if (fpsCount == 30)
        {
            fps.text = (fpsC / 30).ToString("FPS: " + "0");
            fpsC = 0;
            fpsCount = 0;
        }
        OilLevel(GameManager.instance.oilLvl);
    }
    private void Start()
    {
        //audio.Play();
        Time.timeScale = 1f;
        audioManager.SliderController();
    }

    public void OilLevel(float oilLvl)
    {
        this.oilLvl.value = oilLvl;
    }

    public void UpdateCoin(int coin)
    {
        coinScoreText.text = coin.ToString();
    }
    
    public void PauseMenuPrinter()
    {
        if (PauseMenu.gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        GameManager.instance.sfx.PlayOneShot(GameManager.instance.sfxClip);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        BaseController.instance.source.Pause();
    }
    public void Resume()
    {
        GameManager.instance.sfx.PlayOneShot(GameManager.instance.sfxClip);
        gameOverPanel.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        BaseController.instance.source.Play();
        gameIsPaused = false;
        StartCoroutine(GameManager.instance.StartGame());
    }
    public void LoadMenu()
    {
        GameManager.instance.sfx.PlayOneShot(GameManager.instance.sfxClip);
        //audio.Stop();
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void WatchAd()
    {
        AdsController.instance.ShowVideo();
        GameManager.instance.sfx.PlayOneShot(GameManager.instance.sfxClip);
    }
    int minSceneId = 1;
    int maxSceneId = 4;
    public void Restart()
    {
        GameManager.instance.sfx.PlayOneShot(GameManager.instance.sfxClip);
        Time.timeScale = 1f;
        int index = MyRandom(minSceneId, maxSceneId);
        //GameManager.instance.StartDriving(index);
        SceneManager.LoadScene(index);
    }
    int randomSeed = 0;
    int MyRandom(int min, int max)
    {
        randomSeed++;
        Random.InitState(randomSeed += (int)System.DateTime.Now.Millisecond);

        return Random.Range(min, max);
    }
}
