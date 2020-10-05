using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutuionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
           string option = resolutions[i].width + " x " + resolutions[i].height;
           options.Add(option);
           if (resolutions[i].width == Screen.width && 
                resolutions[i].height == Screen.height){
               currentResolutuionIndex = i;
               Debug.Log(resolutions[i].width + ", " + resolutions[i].height);
           }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutuionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume (float volume){

        audioMixer.SetFloat("Volume", volume);

    }

    public void SetQuality (int qualityIndex){

        QualitySettings.SetQualityLevel(qualityIndex);
    }


    public void SetFullScreen (bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution (int resolutionIndex){

        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);

    }

}
