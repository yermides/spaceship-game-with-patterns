// Only responsible for telling the settings manager to actually load and save the settings from and to a file
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameSettingsInstaller : MonoBehaviour
{
    [SerializeField]
    private SettingsManager settingsManager;

    [SerializeField]
    SettingsMenuUIHandler settingsMenuUIHandler;

    /*

    private GameSettingsSO settings;

    private void Awake() 
    {
        settings = GameSettingsSO.FindUserSettings();
    }

    private void Start() 
    {
        settingsManager.ApplySettings(settings);
    }

    private void OnEnable() 
    {
        // settingsManager.OnVolumeChanged     += settingsMenuUIHandler.SetVolumeFromUI;
        settingsManager.OnVolumeChanged     += settings.SetVolume;

        settingsManager.OnQualityChanged    += settings.SetQuality;
        // settingsManager.OnQualityChanged    += settingsMenuUIHandler.SetQualityFromUI;

        // settingsManager.OnResolutionChanged += ; // TODO: 

        // settingsManager.OnFullscreenChanged += settingsMenuUIHandler.SetFullscreenFromUI;
        settingsManager.OnFullscreenChanged += settings.SetFullscreen;

        // settingsManager.OnFramerateChanged  += settingsMenuUIHandler.SetFPSCountFromUI;
        settingsManager.OnFramerateChanged  += settings.SetTargetFrameRate;

        // settingsManager.OnVSyncCountChanged += settingsMenuUIHandler.SetVSyncCountFromUI;
        settingsManager.OnVSyncCountChanged += settings.SetVSyncCount;
    }

    private void OnDisable() 
    {
        // settingsManager.OnVolumeChanged     -= settingsMenuUIHandler.SetVolumeFromUI;
        settingsManager.OnVolumeChanged     -= settings.SetVolume;

        // settingsManager.OnQualityChanged    -= settingsMenuUIHandler.SetQualityFromUI;
        settingsManager.OnQualityChanged    -= settings.SetQuality;

        // settingsManager.OnResolutionChanged -= ; // TODO: 

        // settingsManager.OnFullscreenChanged -= settingsMenuUIHandler.SetFullscreenFromUI;
        settingsManager.OnFullscreenChanged -= settings.SetFullscreen;

        // settingsManager.OnFramerateChanged  -= settingsMenuUIHandler.SetFPSCountFromUI;
        settingsManager.OnFramerateChanged  -= settings.SetTargetFrameRate;

        // settingsManager.OnVSyncCountChanged -= settingsMenuUIHandler.SetVSyncCountFromUI;
        settingsManager.OnVSyncCountChanged -= settings.SetVSyncCount;
    }

    */
}
