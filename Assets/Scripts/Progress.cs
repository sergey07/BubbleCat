using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string CurrentSceneName;
    public int LevelBuildIndex = 0;
    public int Score;
    public int ChestCount;
    public bool IsSoundOn = true;
    public int JoystickPos = 2;
}

public class Progress: MonoBehaviour
{
    public PlayerInfo PlayerInfo;

#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);
#endif

    public static Progress Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            // DontDestroyOnLoad works only on root GameObjects
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        LoadExtern();
#else
        Debug.Log("LoadExtern");
        PlayerInfo.CurrentSceneName = PlayerPrefs.GetString("LoadExtern");
        PlayerInfo.LevelBuildIndex = PlayerPrefs.GetInt("LevelBuildIndex");
        PlayerInfo.Score = PlayerPrefs.GetInt("Score");
        PlayerInfo.ChestCount = PlayerPrefs.GetInt("ChestCount");
        PlayerInfo.IsSoundOn = PlayerPrefs.GetInt("IsSoundOn") == 1;
        PlayerInfo.JoystickPos = PlayerPrefs.GetInt("JoystickPos");
#endif
    }

    public void Save()
    {
        Debug.Log("SaveExtern");
        PlayerPrefs.SetString("CurrentSceneName", PlayerInfo.CurrentSceneName);
        PlayerPrefs.SetInt("LevelBuildIndex", PlayerInfo.LevelBuildIndex);
        PlayerPrefs.SetInt("Score", PlayerInfo.Score);
        PlayerPrefs.SetInt("ChestCount", PlayerInfo.ChestCount);
        PlayerPrefs.SetInt("IsSoundOn", PlayerInfo.IsSoundOn ? 1 : 0);
        PlayerPrefs.SetInt("JoystickPos", PlayerInfo.JoystickPos);
        PlayerPrefs.Save();

#if !UNITY_EDITOR && UNITY_WEBGL
        // Пытаемся отправить в лидерборд, если авторизованы
        if (Yandex.Instance.IsAuthorized)
        {
            string jsonString = JsonUtility.ToJson(PlayerInfo);
            SaveExtern(jsonString);
            SetToLeaderboard(PlayerInfo.Score);
            Debug.Log("Score submitted to leaderboard");
        }
        else
        {
            Debug.Log("Score not submitted to leaderboard (not authorized)");
            // Можно показать кнопку "Авторизоваться для сохранения в лидерборд"
        }
#else
        Debug.Log("SetToLeaderboard");
#endif

    }

    public void SetPlayerInfo(string value)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
#endif
    }
}
