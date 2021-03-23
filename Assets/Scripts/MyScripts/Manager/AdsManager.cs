using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{ 
    private string store_id = "3091210";

    private string rewardVideo_ad = "rewardedVideo";

    public Button carsButton;
    public GameObject adsButton;

    private void Start()
    {
        Advertisement.Initialize(store_id, true);
    }

    public void ShowVideo()
    {
        if (Advertisement.IsReady(rewardVideo_ad))
        {
            var options = new ShowOptions { resultCallback = UnlockCars };
            Advertisement.Show(rewardVideo_ad, options);
        }
    }
     public void UnlockCars(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:

                //if ads watched bonuses should give or money 10$
                carsButton.interactable = true;
                adsButton.SetActive(false);
                PlayerPrefs.Save();
                break;
        }

    }
}
