using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void GameplayApiStart();
    private static extern void GameplayApiStop();
#endif

    private bool _isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Pause()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    GameplayApiStop();
#endif

        if (_isPaused)
        {
            return;
        }

        SoundManager.Instance.Mute(true);

        _isPaused = true;
        Time.timeScale = 0;
        TimerManager.Instance.StopTimer();
    }

    public void Resume()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    GameplayApiStart();
#endif

        if (!_isPaused)
        {
            return;
        }

        SoundManager.Instance.Mute(!Progress.Instance.PlayerInfo.IsSoundOn);

        _isPaused = false;
        Time.timeScale = 1.0f;
        TimerManager.Instance.ResumeTimer();
    }
}
