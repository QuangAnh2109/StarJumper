// SettingsManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Components")]
    public AudioMixer masterMixer;
    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    void Start()
    {
        SetupQualityDropdown();
        LoadSettings();
    }

    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    private void LoadSettings()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = volume;
        SetVolume(volume);

        int qualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        qualityDropdown.value = qualityLevel;
        SetQuality(qualityLevel);

        bool isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 0) == 1;
        fullscreenToggle.isOn = isFullscreen;
        SetFullscreen(isFullscreen);
    }

    private void SetupQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        List<string> options = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(options);
        qualityDropdown.RefreshShownValue();
    }
}