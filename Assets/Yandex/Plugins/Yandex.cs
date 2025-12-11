using System;
using UnityEngine;
using YG;

public class Yandex : MonoBehaviour
{
    public static Yandex Instance { get; private set; }

    public bool IsAuthorized { get; private set; } = false;
    public string PlayerId { get; private set; }
    public string PlayerName { get; private set; }

    public event Action OnAuthSuccess;
    public event Action<string> OnAuthFailed;
    public event Action OnAuthStatusUpdated;


#if !UNITY_EDITOR && UNITY_WEBGL
    //[DllImport("__Internal")]
    //private static extern void InitYandexSDKExtern();

    //[DllImport("__Internal")]
    //private static extern void LoadingApiReadyExtern();

    //[DllImport("__Internal")]
    //private static extern void GameplayApiStartExtern();

    //[DllImport("__Internal")]
    //private static extern void GameplayApiStopExtern();

    //[DllImport("__Internal")]
    //private static extern void RequestAuthorizationExtern();

    ////[DllImport("__Internal")]
    ////private static extern void FetchPlayerDataExtern();

    //[DllImport("__Internal")]
    //private static extern void ResurrectExtern();

    //[DllImport("__Internal")]
    //private static extern void RateGameExtern();
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
        //InitYandexSDKExtern();
        
#else
        Debug.LogWarning("Yandex SDK initialization works only in WebGL build");
#endif
    }

    public void GameplayApiStart()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        YG2.GameplayStart();
#else
        Debug.LogWarning("Yandex SDK GameplayApiStart works only in WebGL build");
#endif
    }

    public void GameplayApiStop()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        YG2.GameplayStop();
#else
        Debug.LogWarning("Yandex SDK GameplayApiStop works only in WebGL build");
#endif
    }

    public void LoadingApiReady()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        YG2.GameReadyAPI();
#else
        Debug.LogWarning("Yandex SDK LoadingApiReady works only in WebGL build");
#endif
    }

    // Запрос авторизации (опциональный)
    public void RequestAuthorization()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YG2.OpenAuthDialog();
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
        //ResurrectExtern();
#endif
        string rewardID = "resurrect"; // Передача id требуется для внутренней работы плагина

        YG2.RewardedAdvShow(rewardID, () =>
        {
            if (rewardID == "resurrect")
            {
                LevelManager.Instance.Resurrect();
            }
        });
    }

    public void RateGame()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        YG2.ReviewShow();
#endif
    }
}
