using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadMasterVolume();
        }
        else
        {
            SetMasterVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetsfxVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetsfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }

    private void LoadMasterVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");

        SetMasterVolume();
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetsfxVolume();
    }

    private void Update()
    {
        SetMusicVolume();
        SetMasterVolume();
        SetsfxVolume();
    }
}
