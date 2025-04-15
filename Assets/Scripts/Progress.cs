using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string CurrentSceneName;
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
#endif
    }

    public void Save()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
        SetToLeaderboard(PlayerInfo.Score);
#else
        Debug.Log("SaveExtern");
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
