using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    UIManager uiManager;

    public static bool gameIsPaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void Pause()
    {
        uiManager.pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Resume()
    {
        uiManager.pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
   
}
