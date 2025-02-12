using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Sprites")]
    [SerializeField] Sprite _doorToFinish;

    [Header("Game Objects")]
    [SerializeField] private GameObject _finishTrigger;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private GameObject _chestCounterPanel;
    [SerializeField] private TextMeshProUGUI _txtChestCounter;

    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipFinishLevel;

    private int _sceneCount;
    private string _currentSceneName;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            //transform.parent = null;
            //DontDestroyOnLoad(gameObject);
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
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

        if (_chestCounterPanel != null)
        {
            if (_currentSceneName == "StartScene")
            {
                _chestCounterPanel.SetActive(false);
            }
            else
            {
                _chestCounterPanel.SetActive(true);
                int allChestCount = Progress.Instance.PlayerInfo.LevelChestCount + Progress.Instance.PlayerInfo.ChestCount;
                _txtChestCounter.text = allChestCount.ToString();
            }
        }

        if (_currentSceneName == "Level" + (_sceneCount - 3))
        {
            _finishTrigger.GetComponent<SpriteRenderer>().sprite = _doorToFinish;
        }
    }

    public string GetCurrentSceneName()
    {
        return _currentSceneName;
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
        Player.Instance.SetPlayerStatus(PlayerStatus.InGame);

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void FinishLevel()
    {
        _audioSource.PlayOneShot(_audioClipFinishLevel);
        StartCoroutine(LoadNextLevel(_audioClipFinishLevel.length));
    }

    public void LoadCatDiedScene()
    {
        Progress.Instance.PlayerInfo.LevelChestCount = 0;

        Progress.Instance.PlayerInfo.CurrentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("CatDied");

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddReward(int reward)
    {
        Progress.Instance.PlayerInfo.LevelChestCount += reward;
        int allChestCount = Progress.Instance.PlayerInfo.LevelChestCount + Progress.Instance.PlayerInfo.ChestCount;
        _txtChestCounter.text = allChestCount.ToString();
    }

    IEnumerator LoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        _currentSceneName = SceneManager.GetActiveScene().name;
        Progress.Instance.PlayerInfo.ChestCount += Progress.Instance.PlayerInfo.LevelChestCount;
        Progress.Instance.PlayerInfo.LevelChestCount = 0;

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
        return SceneManager.GetActiveScene().buildIndex;
    }
}
