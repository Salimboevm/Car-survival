using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buying : MonoBehaviour
{
    public GameObject priceButton;
    public string name;
    public Button interactableButton;
    private void Start()
    {
        //GetComponent<Button>().interactable = IntToBool(PlayerPrefs.GetInt(name, 0));
        interactableButton.interactable = IntToBool(PlayerPrefs.GetInt(name, 0));
        priceButton.SetActive(!interactableButton.interactable);
    }
    public void Buy(int price)
    {
        if (GameManager.instance.coins >= price)
        {
            GameManager.instance.coins -= price;
            PlayerPrefs.SetInt(name, BoolToInt(interactableButton.interactable = true));
            priceButton.SetActive(!interactableButton.interactable);
            PlayerPrefs.Save();
        }
        else
        {
            return;
            //GameManager.instance.coins = GameManager.instance.coins;
        }
            //PlayerPrefs.SetInt(name, BoolToInt(GetComponent<Button>().interactable));
    }

    int BoolToInt(bool bTrue)
    {
        if (bTrue)
            return 1;
        else
            return 0;
    }
    bool IntToBool(int iTrue)
    {
        if (iTrue == 1)
            return true;
        else
            return false;
    }
}


