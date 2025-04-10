using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Sprites")]
    [SerializeField] Sprite _doorToFinish;

    [Header("Game Objects")]
    [SerializeField] private GameObject _finishTrigger;
    [SerializeField] private GameObject _finishLevelPanel;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    
    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipFinishLevel;

    private int _sceneCount;
    private string _currentSceneName;

    private AudioSource _audioSource;

    private bool _isPaused = false;
    private float _oldTimeScale;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            transform.parent = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        _currentSceneName = SceneManager.GetActiveScene().name;

        if (_txtLevel != null)
        {
            int levelNumber = GetCurrentLevelNumber();

            if (Language.Instance.CurrentLanguage == "en")
            {
                _txtLevel.text = "Level " + levelNumber;
            }
            else if(Language.Instance.CurrentLanguage == "ru")
            {
                _txtLevel.text = "Уровень " + levelNumber;
            }
            else
            {
                _txtLevel.text = "Level " + levelNumber;
            }
        }

        if (_currentSceneName == "Level" + (_sceneCount - 3))
        {
            _finishTrigger.GetComponent<SpriteRenderer>().sprite = _doorToFinish;
        }
    }

    public void Pause()
    {
        if (_isPaused)
        {
            return;
        }

        _isPaused = true;
        _oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
        TimerManager.Instance.StopTimer();
    }

    public void Resume()
    {
        if (!_isPaused)
        {
            return;
        }

        _isPaused = false;
        Time.timeScale = _oldTimeScale;
        TimerManager.Instance.ResumeTimer();
    }

    public string GetCurrentSceneName()
    {
        return _currentSceneName;
    }

    public void LoadFirstLevel()
    {
        Progress.Instance.PlayerInfo.Score = 0;
        Progress.Instance.PlayerInfo.ChestCount = 0;
#if !UNITY_EDITOR && UNITY_WEBGL
        Progress.Instance.Save();
#endif
        SceneManager.LoadScene("Level1");
        Player.Instance.SetPlayerStatus(PlayerStatus.InGame);

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void FinishLevel()
    {
        TimerManager.Instance.StopTimer();
        _finishLevelPanel.SetActive(true);
        int chestCount = Progress.Instance.PlayerInfo.ChestCount;
        int scoreForChest = ChestManager.Instance.GetScoreForChest();
        int remainingTime = TimerManager.Instance.GetRemainingSeconds();
        _finishLevelPanel.GetComponent<FinishLevel>().UpdatePanel(chestCount, scoreForChest, remainingTime);
        _audioSource.PlayOneShot(_audioClipFinishLevel);
        StartCoroutine(LoadNextLevel(_audioClipFinishLevel.length));
    }

    public void LoadCatDiedScene()
    {
        Progress.Instance.PlayerInfo.ChestCount = 0;

        Progress.Instance.PlayerInfo.CurrentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("CatDied");

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        _currentSceneName = SceneManager.GetActiveScene().name;

        Progress.Instance.PlayerInfo.CurrentSceneName = _currentSceneName;
        Progress.Instance.PlayerInfo.ChestCount = 0;

#if !UNITY_EDITOR && UNITY_WEBGL
        Progress.Instance.Save();
#endif

        if (_currentSceneName != "Level" + (_sceneCount - 3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            SceneManager.LoadScene("FinishGame");
        }

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    private int GetCurrentLevelNumber()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }
}
