using System;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "3DURP/GameSettingsSO", order = 0)]
public class GameSettingsSO : ScriptableObject 
{
    private const string kFILENAME = "CustomGameSettings.dat";

    private string Filepath {
        get {
            return $"./Assets/Resources/SerializedJsonData/{kFILENAME}";
            // return $"./{kFILENAME}";
        }
    }

    public Vector2Int targetResolution = new Vector2Int(800, 600); // x = width, y = height

    [Range(30, 165)]
    public int targetFrameRate = 60;

    [Range(0, 2)]
    public int vSyncCount = 0;

    public bool wantedFullscreen = false;

    [Range(0.0f, 1.0f)]
    public float generalVolume = 1.0f;

    [Range(0, 2)] // set range between quality levels in player settings
    public int wantedQualityIndex = 0;

    private void Awake() 
    {
        LoadFromDisk();
    }

    public static GameSettingsSO FindUserSettings()
    {
        // Deprecated, using PlayerPrefs instead

        GameSettingsSO settings = Resources.Load<GameSettingsSO>("ScriptableObjectsData/CurrentGameSettings");

        #if UNITY_EDITOR

        if(!settings) 
        {
            Debug.LogWarning("Failed to load current settings, creating asset from default settings...");

            settings = Instantiate(Resources.Load<GameSettingsSO>("ScriptableObjectsData/DefaultGameSettings"));
            AssetDatabase.CreateAsset(settings, "Assets/Resources/ScriptableObjectsData/CurrentGameSettings.asset");
            AssetDatabase.SaveAssets();
            // GameSettingsSO asset = ScriptableObject.CreateInstance<GameSettingsSO>();
        }
        
        #endif

        return settings;
    }

    public void SetResolution(Vector2Int res)
    {
        SetResolution(res.x, res.y);
    }

    public void SetResolution(int width, int height)
    {
        targetResolution.x = width;
        targetResolution.y = height;
    }

    public void SetResolution(Resolution resolution)
    {
        targetResolution.x = resolution.width;
        targetResolution.y = resolution.height;
    }

    public void SetTargetFrameRate(int framerate) => targetFrameRate = framerate;
    public void SetQuality(int qualityIndex) => wantedQualityIndex = qualityIndex;
    public void SetVSyncCount(int count) => vSyncCount = count;
    public void SetFullscreen(bool fullScreen) => wantedFullscreen = fullScreen;
    public void SetVolume(float volume) => generalVolume = volume;

    private void Copy(GameSettingsSO other) 
    {
        SetTargetFrameRate(other.targetFrameRate);
        SetQuality(other.wantedQualityIndex);
        SetVSyncCount(other.vSyncCount);
        SetFullscreen(other.wantedFullscreen);
        SetVolume(other.generalVolume);
    }

    public void LoadFromDisk()
    {
        Vector2Int res = new Vector2Int(
                PlayerPrefs.GetInt(PlayerPrefsHelper.ScreenWidth, 800)
            ,   PlayerPrefs.GetInt(PlayerPrefsHelper.ScreenHeight, 600)
        );

        bool fs = Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefsHelper.FullScreen, 0)); // false by default
        int vs = PlayerPrefs.GetInt(PlayerPrefsHelper.VSyncCount, 0);
        int q = PlayerPrefs.GetInt(PlayerPrefsHelper.Quality, 0);
        int fr = PlayerPrefs.GetInt(PlayerPrefsHelper.Framerate, 60);

        SetResolution(res);
        SetFullscreen(fs);
        SetVSyncCount(vs);
        SetQuality(q);
        SetTargetFrameRate(fr);

        #region Old version -> Loading from file

        /*
        Debug.Log("LoadFromFile");
        // string filePath = Path.Combine(Application.persistentDataPath, kFILENAME);

        if(!File.Exists(Filepath))
        {
            Debug.LogWarning($"File \"{Filepath}\" not found!", this);
            return;
        }

        string jsonText = File.ReadAllText(Filepath);
        JsonUtility.FromJsonOverwrite(jsonText, this);
        */

        #endregion
    }

    public void WriteToDisk() 
    {
        PlayerPrefs.SetInt(PlayerPrefsHelper.ScreenWidth, targetResolution.x);
        PlayerPrefs.SetInt(PlayerPrefsHelper.ScreenHeight, targetResolution.y);
        PlayerPrefs.SetInt(PlayerPrefsHelper.FullScreen, Convert.ToInt32(wantedFullscreen));
        PlayerPrefs.SetInt(PlayerPrefsHelper.VSyncCount, vSyncCount);
        PlayerPrefs.SetInt(PlayerPrefsHelper.Quality, wantedQualityIndex);
        PlayerPrefs.SetInt(PlayerPrefsHelper.Framerate, targetFrameRate);

        #region Old version -> Writing to file
        /*
        Debug.Log("WriteToFile");

        if(!File.Exists(Filepath))
        {
            File.Create(Filepath);
        }
       
        string jsonText = JsonUtility.ToJson(this);
        File.WriteAllText(Filepath, jsonText);
        */
        #endregion
    }

    // private void Reset() 
    // {
    //     GameSettingsSO defaultSettings = Resources.Load<GameSettingsSO>("ScriptableObjectsData/DefaultGameSettings");
        
    //     targetResolution = defaultSettings.targetResolution;
    //     targetFrameRate = defaultSettings.targetFrameRate;
    //     vSyncCount = defaultSettings.vSyncCount;
    //     wantedFullscreen = defaultSettings.wantedFullscreen;
    //     wantedQualityIndex = defaultSettings.wantedQualityIndex;
    // }
}
