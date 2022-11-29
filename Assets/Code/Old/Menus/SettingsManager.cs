// Only responsible for calling unity's functions
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using System.Linq;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements References")]

    [SerializeField]
    private AudioMixer globalMixer;

    [Header("Private elements")]

    [SerializeField]
    private GameSettingsSO settings;

    [SerializeField]
    private bool loadSettingsOnStart = true;

    // private Resolution[] avaliableResolutions;
    private List<Vector2Int> avaliableResolutions2 = new List<Vector2Int>();

    public List<Vector2Int> AvailableResolutions 
    {
        get 
        { 
            if(avaliableResolutions2.Count == 0)
            {
                FillResolutions();
            }
            
            return avaliableResolutions2; 
        }
    }

    public int kUnknownResolutionIndex 
    { 
        get { return -1; } 
    }

    [Header("Events")]
    public UnityAction<float> OnVolumeChanged;
    public UnityAction<int> OnQualityChanged;
    public UnityAction<int> OnResolutionChanged;
    public UnityAction<bool> OnFullscreenChanged;
    public UnityAction<int> OnFramerateChanged;
    public UnityAction<int> OnVSyncCountChanged;

    private void Awake() 
    {
        FillResolutions();
    }

    private void Start() 
    {
        if(!settings) 
        {
            settings = GameSettingsSO.FindUserSettings();
        }

        if(loadSettingsOnStart)
        {
            settings.LoadFromDisk();
            ApplySettings(settings);
        }
    }

    // private void OnGUI() 
    // {
    //     GUILayout.Label($"Unique res: {AvailableResolutions.Count}");
    // }
    
    public void SetVolume(float volume) 
    {
        // TODO: change audio mixer params

        // Invoke the associated events (mainly from gui)
        OnVolumeChanged?.Invoke(volume);

        // Save our data in the scriptable object as we go
        settings.generalVolume = volume;
    }

    public void SetQuality(int index) 
    {
        // int level = QualitySettings.GetQualityLevel();
        // Debug.Log($"Current level is {level} and I want to change to {index}");

        QualitySettings.SetQualityLevel(index);
        OnQualityChanged?.Invoke(index);
        settings.SetQuality(index);
    }

    public void SetResolution(int index) 
    {
        Vector2Int resolution = AvailableResolutions[index];
        Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
        OnResolutionChanged?.Invoke(index);
        settings.SetResolution(resolution.x, resolution.y);

        // Resolution resolution = avaliableResolutions[index];
        // Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        // OnResolutionChanged?.Invoke(new Vector2Int(resolution.width, resolution.height), index);
        // settings.SetResolution(resolution.width, resolution.height);
    }

    public void SetResolution(Vector2Int resolution)
    {
        int index = AvailableResolutions.IndexOf(resolution);
        SetResolution(index);

        // Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
        // OnResolutionChanged?.Invoke(resolution, kUnknownResolutionIndex);
        // settings.SetResolution(resolution.x, resolution.y);
    }

    public void SetFullscreen(bool fullscreen) 
    {
        Screen.fullScreen = fullscreen;

        OnFullscreenChanged?.Invoke(fullscreen);

        settings.SetFullscreen(fullscreen);
    }

    public void SetFPSCount(float fps) 
    {
        int fpsInt = (int)fps;
        Application.targetFrameRate = fpsInt;

        OnFramerateChanged?.Invoke(fpsInt);

        settings.SetTargetFrameRate(fpsInt);
    }

    public void SetVSyncCount(int vSyncCount)
    {
        QualitySettings.vSyncCount = vSyncCount;

        OnVSyncCountChanged?.Invoke(vSyncCount);

        settings.SetVSyncCount(vSyncCount);
    }

    //private bool EqualsResolution(Resolution r1, Resolution r2) => (r1.height == r2.height && r1.width == r2.width);

    public void ApplySettings()
    {
        if(!settings) return;

        ApplySettings(settings);
    }

    public void ApplySettings(GameSettingsSO settings) 
    {
        SetVolume(settings.generalVolume);
        SetQuality(settings.wantedQualityIndex);
        SetResolution(settings.targetResolution);
        SetFullscreen(settings.wantedFullscreen);
        SetFPSCount(settings.targetFrameRate);
        SetVSyncCount(settings.vSyncCount);
    }

    public void SaveSettings()
    {
        settings.WriteToDisk();
    }

    private void FillResolutions()
    {
        // Load distinct resolutions
        HashSet<Vector2Int> uniqueResolutions = new HashSet<Vector2Int>();

        foreach(Resolution resolution in Screen.resolutions) 
        {
            uniqueResolutions.Add(new Vector2Int(resolution.width, resolution.height));
        }

        avaliableResolutions2 = uniqueResolutions.ToList();

        Debug.LogWarning(uniqueResolutions.Count);

        // avaliableResolutions = Screen.resolutions;
    }
}
