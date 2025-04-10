using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void FetchPlayerDataExtern();

    [DllImport("__Internal")]
    private static extern void RateGameExtern();
#endif

    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private RawImage _avatar;

    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        InitPlayerData();
#endif
    }

    public void InitPlayerData()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        FetchPlayerDataExtern();
#endif
    }

    public void SetPlayerName(string playerName)
    {
        _playerName.text = playerName;
    }

    public void SetAvatar(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    public void RateGameButton()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        RateGameExtern();
#endif
    }

    IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            _avatar.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
