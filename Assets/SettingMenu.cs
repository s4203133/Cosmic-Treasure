using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {


    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    //Resolution Options

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        //resolutionDropdown.ClearOptions();

        List<string> LoadOptions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions [i].width + "x" + resolutions[i].height;
            //Options.Add(option);


            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height== Screen.currentResolution.height) 
            {
                currentResolutionIndex = i;
            }
        }

        
        //resolutionsDropdown.AppOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    { 
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // Volume Options
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


    //Graphics Options

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Fullscreen Option

    public void SetFullscreen (bool isFullscreen)
    { 
         Screen.fullScreen = isFullscreen;
    }



}
