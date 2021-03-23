using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Space(10)]
    public Slider musicSlider;
    [Space(10)]
    public Slider sfxSlider;

    float musicVolume;
    float sfxVolume;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }
    public void SetSFXVolume(float sfxVolume)
    {
        audioMixer.SetFloat("sfxVolume", sfxVolume);
    }

    private void Start()
    {
        SliderController();
    }

    public void SliderController()
    {
        //SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 0));
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", -50f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", -50f);
 
    }
    private void OnEnable()
    {
        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("sfxVolume", out sfxVolume);
    }
    private void OnDisable()
    {   
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }
    

}
