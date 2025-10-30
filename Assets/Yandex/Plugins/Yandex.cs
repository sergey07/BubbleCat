using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    public static Yandex Instance { get; private set; }

    public bool IsAuthorized { get; private set; }
    public string PlayerId { get; private set; }
    public string PlayerName { get; private set; }

    public event Action OnAuthSuccess;
    public event Action<string> OnAuthFailed;
    public event Action OnAuthStatusUpdated;


#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void InitYandexSDKExtern();

    [DllImport("__Internal")]
    private static extern void LoadingApiReadyExtern();

    [DllImport("__Internal")]
    private static extern void GameplayApiStartExtern();

    [DllImport("__Internal")]
    private static extern void GameplayApiStopExtern();

    [DllImport("__Internal")]
    private static extern void RequestAuthorizationExtern();

    //[DllImport("__Internal")]
    //private static extern void FetchPlayerDataExtern();

    [DllImport("__Internal")]
    private static extern void ResurrectExtern();

    [DllImport("__Internal")]
    private static extern void RateGameExtern();
#endif

    [Serializable]
    private class AuthStatus
    {
        public bool authorized;
        public PlayerData player;
    }

    [Serializable]
    private class PlayerData
    {
        public string id;
        public string name;
    }

    //[SerializeField] private TextMeshProUGUI _playerName;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitYandexSDK() {
#if !UNITY_EDITOR && UNITY_WEBGL
        InitYandexSDKExtern();
#else
        Debug.LogWarning("Yandex SDK initialization works only in WebGL build");
#endif
    }

    public void GameplayApiStart()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        GameplayApiStartExtern();
#else
        Debug.LogWarning("Yandex SDK GameplayApiStart works only in WebGL build");
#endif
    }

    public void GameplayApiStop()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        GameplayApiStopExtern();
#else
        Debug.LogWarning("Yandex SDK GameplayApiStop works only in WebGL build");
#endif
    }

    public void LoadingApiReady()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        LoadingApiReadyExtern();
#else
        Debug.LogWarning("Yandex SDK LoadingApiReady works only in WebGL build");
#endif
    }

    // ������ ����������� (������������)
    public void RequestAuthorization()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestAuthorizationExtern();
#else
        Debug.LogWarning("Yandex Games auth works only in WebGL build");
        OnAuthFailed?.Invoke("Not in WebGL environment");
#endif
    }
    //public void InitPlayerData()
    //{
#if !UNITY_EDITOR && UNITY_WEBGL
        //FetchPlayerDataExtern();
#endif
    //}

    //public void SetPlayerName(string playerName)
    //{
    //    if (_playerName != null)
    //    {
    //        _playerName.text = playerName;
    //    }
    //}

    public void Resurrect()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        ResurrectExtern();
#endif
    }

    public void RateGame()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        RateGameExtern();
#endif
    }
}
