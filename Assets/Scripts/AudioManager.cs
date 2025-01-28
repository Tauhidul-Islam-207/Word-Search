using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        CheckMusicVolume();
        CheckSFXVolume();
    }

    //This method is used to set the music volume.
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    //This method is used to set the sfx volume.
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
  
    //This method is used to load music volume from saved data.
    public void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }
   
    //This method is used to load sfx volume from saved data.
    public void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume();
    }
  
    //This method is used to check for any music volume saved data.
    public void CheckMusicVolume()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }
  
    //This method is used to check for any sfx volume saved data.
    public void CheckSFXVolume()
    {
        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }
}



