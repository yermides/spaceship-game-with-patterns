// Only responsible for connecting to the events from settings manager and updating the UI accordingly (and vice-versa)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEditor;

public class SettingsMenuUIHandler : MonoBehaviour
{
    [Header("Object References")]

    [SerializeField]
    private SettingsManager settingsManager;

    [Header("UI Elements References")]

    // The elements and their events are connected to SettingsManager via the editor

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown qualityDropdown;

    [SerializeField]
    private Toggle fullscreenToggle;

    [SerializeField]
    private Slider framerateSlider;

    private void Awake() 
    {
        // setup: load resolution data as options for the dropdowns, etc
        if(!resolutionDropdown)
        {
            Debug.LogWarning("resolutionDropdown is not set");
        }

        List<Vector2Int> resolutions = settingsManager.AvailableResolutions;

        resolutionDropdown.ClearOptions();

        List<string> resolutionNames = new List<string>();

        for (int i = 0; i < resolutions.Count; i++)
        {
            Vector2Int resolution = resolutions[i];
            resolutionNames.Add($"{ resolution.x } x { resolution.y }");
        }

        resolutionDropdown.AddOptions(resolutionNames);
        // Vector2Int current = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        // Debug.LogWarning(resolutions.IndexOf(current));
        // resolutionDropdown.value = resolutions.IndexOf(current);
        resolutionDropdown.value = 0;
        resolutionDropdown.RefreshShownValue();

        // setup quality dropdown
        qualityDropdown.ClearOptions();
        List<string> qualityNames = new List<string>();

        foreach (string name in QualitySettings.names)
        {
            qualityNames.Add(name);
        }

        qualityDropdown.AddOptions(qualityNames);
        // qualityDropdown.value = 0;
        qualityDropdown.RefreshShownValue();
    }

    private void OnEnable() 
    {
        settingsManager.OnVolumeChanged     += UpdateVolumeGUI;
        settingsManager.OnQualityChanged    += UpdateQualityGUI;
        settingsManager.OnResolutionChanged += UpdateResolutionGUI;
        settingsManager.OnFullscreenChanged += UpdateFullscreenGUI;
        settingsManager.OnFramerateChanged  += UpdateFramerateGUI;
        settingsManager.OnVSyncCountChanged += UpdateVSyncCountGUI;
    }

    private void OnDisable() 
    {
        settingsManager.OnVolumeChanged     -= UpdateVolumeGUI;
        settingsManager.OnQualityChanged    -= UpdateQualityGUI;
        settingsManager.OnResolutionChanged -= UpdateResolutionGUI;
        settingsManager.OnFullscreenChanged -= UpdateFullscreenGUI;
        settingsManager.OnFramerateChanged  -= UpdateFramerateGUI;
        settingsManager.OnVSyncCountChanged -= UpdateVSyncCountGUI;
    }

    void Update()
    {
        // For some reason it tries to hide the cursor, so force visibility every frame
        Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
    }

    public void UpdateVolumeGUI(float volume) 
    {
        Debug.LogError("UpdateVolumeGUI NOT IMPLEMENTED");
    }

    public void UpdateQualityGUI(int index) 
    {
        qualityDropdown.value = index;

        /*
        settingsManager.OnQualityChanged -= UpdateQualityGUI;
        // qualityDropdown.onValueChanged.RemoveListener(settingsManager.SetQuality);
        qualityDropdown.onValueChanged.RemoveAllListeners();

        qualityDropdown.value = index;
        qualityDropdown.RefreshShownValue();

        qualityDropdown.onValueChanged.AddListener(settingsManager.SetQuality);
        settingsManager.OnQualityChanged += UpdateQualityGUI;

        // Debug.LogWarning(index);
        // Debug.LogWarning(qualityDropdown.value);
        */
    }

    public void UpdateResolutionGUI(int index) 
    {
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();

        // Debug.LogError("UpdateResolutionGUI NOT IMPLEMENTED");
    }

    public void UpdateFullscreenGUI(bool fullscreen) 
    {
        fullscreenToggle.isOn = fullscreen;
    }

    public void UpdateFramerateGUI(int fps)
    {
        UpdateFramerateGUI((float)fps);
    }

    public void UpdateFramerateGUI(float fps) 
    {
        // Debug.Log($"UpdateFramerateGUI {fps}");
        framerateSlider.value = fps;
    }

    public void UpdateVSyncCountGUI(int vSyncCount)
    {
        Debug.LogError("UpdateVSyncCountGUI NOT IMPLEMENTED");
    }
}

#region  Old implementation
/*
 // Only responsible for connecting to the events from settings manager and updating the UI accordingly (and vice-versa)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEditor;

public class SettingsMenuUIHandler : MonoBehaviour
{
   [Header("Object References")]

   [SerializeField]
   private SettingsManager settingsManager;

   [Header("UI Elements References")]

   [SerializeField]
   private TMP_Dropdown resolutionDropdown;

   [SerializeField]
   private TMP_Dropdown qualityDropdown;

   [SerializeField]
   private Toggle fullscreenToggle;

   [Header("Private References")]
   private Resolution[] avaliableResolutions;

   private void Awake() 
   {
       avaliableResolutions = Screen.resolutions;
   }

   private void OnEnable() 
   {
       settingsManager.OnVolumeChanged     += SetVolumeFromUI;
       settingsManager.OnQualityChanged    += SetQualityFromUI;
       //settingsManager.OnResolutionChanged += SetResolutionFromUI;
       settingsManager.OnFullscreenChanged += SetFullscreenFromUI;
       settingsManager.OnFramerateChanged  += SetFPSCountFromUI;
       settingsManager.OnVSyncCountChanged += SetVSyncCountFromUI;
   }

   private void OnDisable() 
   {
       settingsManager.OnVolumeChanged     -= SetVolumeFromUI;
       settingsManager.OnQualityChanged    -= SetQualityFromUI;
       // settingsManager.OnResolutionChanged
       settingsManager.OnFullscreenChanged -= SetFullscreenFromUI;
       settingsManager.OnFramerateChanged  -= SetFPSCountFromUI;
       settingsManager.OnVSyncCountChanged -= SetVSyncCountFromUI;
   }

   void Update()
   {
       // For some reason it tries to hide the cursor, so force visibility every frame
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
   }

   public void SetVolumeFromUI(float volume) 
   {
       // de-subscribe so we dont end up with a recursive loop from event calls
       settingsManager.OnVolumeChanged -= SetVolumeFromUI;
       settingsManager.SetVolume(volume);
       settingsManager.OnVolumeChanged += SetVolumeFromUI;
   }

   public void SetQualityFromUI(int index) 
   {
       settingsManager.OnQualityChanged    -= SetQualityFromUI;
       settingsManager.SetQuality(index);
       settingsManager.OnQualityChanged    += SetQualityFromUI;
   }

   public void SetResolutionFromUI(int index) 
   {
       throw new System.NotImplementedException("NOT IMPLEMENTED");
   }

   public void SetFullscreenFromUI(bool fullscreen) 
   {
       settingsManager.OnFullscreenChanged -= SetFullscreenFromUI;
       settingsManager.SetFullscreen(fullscreen);
       settingsManager.OnFullscreenChanged += SetFullscreenFromUI;
   }

   public void SetFPSCountFromUI(int fps)
   {
       SetFPSCountFromUI((float)fps);
   }

   public void SetFPSCountFromUI(float fps) 
   {
       settingsManager.OnFramerateChanged -= SetFPSCountFromUI;
       settingsManager.SetFPSCount(fps);
       settingsManager.OnFramerateChanged += SetFPSCountFromUI;
   }

   public void SetVSyncCountFromUI(int vSyncCount)
   {
       settingsManager.OnVSyncCountChanged -= SetVSyncCountFromUI;
       settingsManager.SetVSyncCount(vSyncCount);
       settingsManager.OnVSyncCountChanged += SetVSyncCountFromUI;
   }

   // Private functions not from monobehaviour

   private void LoadScreenData()
   {
       LoadResolutions();
       LoadFullscreenState();
   }

   private void LoadResolutions() 
   {
       // if(!resolutionDropdown) return;

       // avaliableResolutions = Screen.resolutions;

       // resolutionDropdown.ClearOptions();

       // int currentResolutionIndex = 0;
       // List<string> resolutionNames = new List<string>();

       // for (int i = 0; i < avaliableResolutions.Length; i++)
       // {
       //     Resolution resolution = avaliableResolutions[i];

       //     resolutionNames.Add($"{ resolution.width } x { resolution.height } ({resolution.refreshRate} Hz)");

       //     if(EqualsResolution(Screen.currentResolution, resolution))
       //     {
       //         currentResolutionIndex = i;
       //         currentSettings.SetResolution(resolution);
       //     }
       // }

       // resolutionDropdown.AddOptions(resolutionNames);
       // resolutionDropdown.value = currentResolutionIndex;
       // resolutionDropdown.RefreshShownValue();
   }

   private void LoadFullscreenState() 
   {
       fullscreenToggle.isOn = Screen.fullScreen;
   }
}
 */

#endregion
