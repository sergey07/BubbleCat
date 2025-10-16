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
    public string PlayerPhotoUrl { get; private set; }

    public event Action OnAuthSuccess;
    public event Action<string> OnAuthFailed;
    public event Action OnAuthStatusUpdated;


#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void InitYandexSDKExtern();

    [DllImport("__Internal")]
    private static extern void RequestAuthorizationExtern();

    //[DllImport("__Internal")]
    //private static extern void FetchPlayerDataExtern();

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
        public string photo;
    }

    //[SerializeField] private TextMeshProUGUI _playerName;
    //[SerializeField] private RawImage _avatar;
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

    //private void Start()
    //{
    //    //InitPlayerData();
    //}

    // Запрос авторизации (опциональный)
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

    //public void SetAvatar(string url)
    //{
    //    if (_avatar != null)
    //    {
    //        StartCoroutine(DownloadImage(url));
    //    }
    //}

    public void RateGameButton()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        RateGameExtern();
#endif
    }

    //IEnumerator DownloadImage(string mediaUrl)
    //{
    //    UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.Log(request.error);
    //    }
    //    else
    //    {
    //        _avatar.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    //    }
    //}
}
