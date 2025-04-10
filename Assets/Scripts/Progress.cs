using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string CurrentSceneName;
    public int Score;
    public int ChestCount;
    public bool IsSoundOn = true;
    public bool IsZoom = false;
    public int DifficultyLvl = 1;
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
#if !UNITY_EDITOR && UNITY_WEBGL
            LoadExtern();
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
        SetToLeaderboard(PlayerInfo.Score);
#endif
    }

    public void SetPlayerInfo(string value)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
#endif
    }
}
