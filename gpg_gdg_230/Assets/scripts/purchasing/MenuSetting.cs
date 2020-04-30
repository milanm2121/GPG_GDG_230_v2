using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSetting : MonoBehaviour
{

    public AudioSource audioSource;

    Resolution[] resolutions;

    public Dropdown resolutionsDropDown;

    public static float staticVolume;

    public static bool firstTime = false;
    public Slider volume_slider;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = currentResolutionIndex;
        resolutionsDropDown.RefreshShownValue();
        if (volume_slider!=null && volume_slider.value == 0.5f && firstTime == false)
        {
            staticVolume = volume_slider.value;
        }
        else if (firstTime == true)
            if(volume_slider!=null)
                volume_slider.value = staticVolume;
            audioSource.volume = staticVolume;
    }


    public void Volume (float volume)
    {
        audioSource.volume = volume;
        staticVolume = volume;
        audioSource.volume = staticVolume;
        firstTime = true;
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolutions (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
