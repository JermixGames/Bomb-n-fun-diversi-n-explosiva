using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playerSelectScreen;
    public GameObject settingsScreen;

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", volume);
    }    
    
    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("volume", volume);
    }



    public void ShowPlayerSelectScreen()
    {
        mainMenu.SetActive(false);
        playerSelectScreen.SetActive(true);
    }

    public void ShowSettingsScreen()
    {
        mainMenu.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void HideSettings()
    {
        settingsScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void HidePlayerSelect()
    {        
        playerSelectScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
