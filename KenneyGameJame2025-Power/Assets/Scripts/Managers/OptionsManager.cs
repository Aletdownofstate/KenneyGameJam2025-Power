using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown windowedModeDropdown; 
    [SerializeField] private TMP_Dropdown frameRateDropdown;
    [SerializeField] private Toggle vSyncToggle;

    [Header("Audio")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider uiVolumeslider;
    [SerializeField] private AudioMixer mixer;

    private float defaultMasterVolume = 0;
    private float defaultMusicVolume = -10;
    private float defaultSfxVolume = 0;
    private float defaultUIVolume = 0;
    const string RESOLUTION_KEY = "Resolution";
    const string WINDOWED_MODE_KEY = "WindowedMode";
    const string FRAME_RATE_KEY = "FrameRate";
    const string VSYNC_KEY = "VSync";
    const string MASTER_VOLUME_KEY = "MasterVolume";
    const string MUSIC_VOLUME_KEY = "MusicVolume";
    const string SFX_VOLUME_KEY = "SoundEffectsVolume";
    const string UI_VOLUME_KEY = "UIVolumeKey";

    private void Start()
    {
        PopulateResolutionDropdown();
        
        LoadOptions();

        SetFrameRate();
        SetWindowedMode();
        SetWindowedMode();

        SetMasterVolume();
        SetMusicVolume();
        SetSoundEffectsVolume();
        SetUIVolume();
    }

    public void SetFrameRate()
    {
        switch (frameRateDropdown.value)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            
            case 1:
                Application.targetFrameRate = 60;
                break;

            case 2:
                Application.targetFrameRate = 90;
                break;

            case 3:
                Application.targetFrameRate = 120;
                break;

            case 4:
                Application.targetFrameRate = 144;
                break;

            case 5:
                Application.targetFrameRate = 165;
                break;

            case 6:
                Application.targetFrameRate = 0;
                break;
        }        

    }

    public void ToggleVsync()
    {
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;
    }

    private void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        Resolution[] resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution res = resolutions[i];
            string option = $"{res.width} x {res.height} @ {res.refreshRateRatio}Hz";
            options.Add(option);

            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetWindowedMode()
    {
        switch (windowedModeDropdown.value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public void SetResolution()
    {
        Resolution selectedResolution = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreenMode, selectedResolution.refreshRateRatio);
    }

    public void SetMasterVolume()
    {
        mixer.SetFloat("masterVolume", masterVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        mixer.SetFloat("musicVolume", musicVolumeSlider.value);
    }

    public void SetSoundEffectsVolume()
    {
        mixer.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }

    public void SetUIVolume()
    {
        mixer.SetFloat("uiVolume", uiVolumeslider.value);
    }

    public void SaveOptions()
    {
        // Save display options

        PlayerPrefs.SetInt(RESOLUTION_KEY, resolutionDropdown.value);
        PlayerPrefs.SetInt(WINDOWED_MODE_KEY, windowedModeDropdown.value);
        PlayerPrefs.SetInt(FRAME_RATE_KEY, frameRateDropdown.value);
        PlayerPrefs.SetInt(VSYNC_KEY, vSyncToggle.isOn ? 1 : 0);

        // Save audio options

        PlayerPrefs.SetInt(MASTER_VOLUME_KEY, (int)masterVolumeSlider.value);
        PlayerPrefs.SetInt(MUSIC_VOLUME_KEY, (int)musicVolumeSlider.value);
        PlayerPrefs.SetInt(SFX_VOLUME_KEY, (int)sfxVolumeSlider.value);
        PlayerPrefs.SetInt(UI_VOLUME_KEY, (int)uiVolumeslider.value);

        PlayerPrefs.Save();

        Debug.Log("Options saved");
    }

    public void LoadOptions()
    {
        // Load display options

        if (PlayerPrefs.HasKey(RESOLUTION_KEY))
            resolutionDropdown.value = PlayerPrefs.GetInt(RESOLUTION_KEY);

        if (PlayerPrefs.HasKey(WINDOWED_MODE_KEY))
            windowedModeDropdown.value = PlayerPrefs.GetInt(WINDOWED_MODE_KEY);

        if (PlayerPrefs.HasKey(FRAME_RATE_KEY))
            frameRateDropdown.value = PlayerPrefs.GetInt(FRAME_RATE_KEY);

        if (PlayerPrefs.HasKey(VSYNC_KEY))
        {
            int vSyncValue = PlayerPrefs.GetInt(VSYNC_KEY);
            QualitySettings.vSyncCount = vSyncValue;
            vSyncToggle.isOn = vSyncValue == 1;
        }

        resolutionDropdown.RefreshShownValue();
        windowedModeDropdown.RefreshShownValue();
        frameRateDropdown.RefreshShownValue();

        // Load audio options

        if (PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
        {
            int volume = PlayerPrefs.GetInt(MASTER_VOLUME_KEY);
            masterVolumeSlider.value = volume;
            mixer.SetFloat("masterVolume", volume);
        }        

        if (PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
        {
            int volume = PlayerPrefs.GetInt(MUSIC_VOLUME_KEY);
            musicVolumeSlider.value = volume;
            mixer.SetFloat("musicVolume", volume);
        }

        if (PlayerPrefs.HasKey(SFX_VOLUME_KEY))
        {
            int volume = PlayerPrefs.GetInt(SFX_VOLUME_KEY);
            sfxVolumeSlider.value = volume;
            mixer.SetFloat("sfxVolume", volume);
        }

        if (PlayerPrefs.HasKey(UI_VOLUME_KEY))
        {
            int volume = PlayerPrefs.GetInt(UI_VOLUME_KEY);
            uiVolumeslider.value = volume;
            mixer.SetFloat("uiVolume", volume);
        }

        Debug.Log("Options loaded");
    }

    public void ResetOptions()
    {
        mixer.SetFloat("masterVolume", defaultMasterVolume);
        mixer.SetFloat("musicVolume", defaultMusicVolume);
        mixer.SetFloat("sfxVolume", defaultSfxVolume);
        mixer.SetFloat("uiVolume", defaultUIVolume);

        masterVolumeSlider.value = defaultMasterVolume;
        musicVolumeSlider.value = defaultMusicVolume;
        sfxVolumeSlider.value = defaultSfxVolume;
        uiVolumeslider.value = defaultUIVolume;

        Debug.Log("Volume options reset");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}