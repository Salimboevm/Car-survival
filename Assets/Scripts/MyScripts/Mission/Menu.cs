using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text coinsText;

    public Text bestScore;


    public GameObject audio;
    
    private void Awake()
    {
        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        //coinsText.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        
        //UpdateCoins((int)BaseController.instance.coins);
    }
    private void Start()
    {
        UpdateScore((int)GameManager.instance.score);
        UpdateCoins();
    }
    public void StartDrive()
    {
        audio.SetActive(false);
    }

    public void UpdateCoins()
    {
        coinsText.text = GameManager.instance.coins.ToString();
        PlayerPrefs.SetInt("Coin", GameManager.instance.coins);
        PlayerPrefs.Save();
        
    }
    public void UpdateScore(int score)
    {
        if(score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore.text = score.ToString();
            PlayerPrefs.Save();
        }
    }
   
}
