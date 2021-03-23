using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    public static AdsController instance;

    private string store_id = "3091210";

    private string video_ad = "video";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Advertisement.Initialize(store_id, true);
    }

    public void ShowVideo()
    {
        if (Advertisement.IsReady(video_ad))
        {
            var options = new ShowOptions {resultCallback = HandleAdResult};
            Advertisement.Show(video_ad, options);
        }
    }

    UIManager uiManager;
    void HandleAdResult(ShowResult result)
    {
        uiManager = FindObjectOfType<UIManager>();
        switch (result)
        {
            case ShowResult.Finished:                
                uiManager.Resume();
                BaseController.instance.rb.gameObject.transform.rotation = Quaternion.identity;
                BaseController.instance.rb.transform.position = new Vector3(0, 0.6f, gameObject.transform.position.z);
                BaseController.instance.source.Play();
                StartCoroutine(GameManager.instance.StartGame());
                BlockerRandom.instance.StartCoroutine(BlockerRandom.instance.UpdateBlock());
                break;
            case ShowResult.Skipped:                
                uiManager.Resume();
                BaseController.instance.rb.gameObject.transform.rotation = Quaternion.identity;
                BaseController.instance.rb.transform.position = new Vector3(0, 0.6f, gameObject.transform.position.z);
                BaseController.instance.source.Play();
                StartCoroutine(GameManager.instance.StartGame());
                BlockerRandom.instance.StartCoroutine(BlockerRandom.instance.UpdateBlock());
                break;
        }
        
    }
}
