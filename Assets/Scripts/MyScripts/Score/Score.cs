using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    private void Update()
    { 
        var car = GameManager.instance.mainPlayerInstance;
        scoreText.text = GameManager.instance.score.ToString("0");
        //scoreText.text = GameManager.instance.score.ToString("Score: " + "0");
    }

}
