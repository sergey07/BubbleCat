using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] private GameObject _pausePanel;

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

    public void GoToStartMenu()
    {
        Yandex.Instance.GameplayApiStop();
        SceneManager.LoadScene("StartMenu");
    }

    public void StartGame()
    {
        Yandex.Instance.GameplayApiStop();
        SceneManager.LoadScene("Tutorial");
    }

    public void Pause()
    {
        Yandex.Instance.GameplayApiStop();

        _isPaused = true;

        SoundManager.Instance.Mute(true);

        Time.timeScale = 0;
        TimerManager.Instance.StopTimer();
    }

    public void Resume()
    {
        Yandex.Instance.GameplayApiStart();

        _isPaused = false;

        bool isMute = !Progress.Instance.PlayerInfo.IsSoundOn;
        SoundManager.Instance.Mute(!Progress.Instance.PlayerInfo.IsSoundOn);

        Time.timeScale = 1.0f;
        TimerManager.Instance.ResumeTimer();
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    public void TogglePause()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void ShowPausePanel()
    {
        _pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        _pausePanel.SetActive(false);
    }
}
