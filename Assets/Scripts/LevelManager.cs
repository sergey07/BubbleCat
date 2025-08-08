using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Sprites")]
    [SerializeField] Sprite _doorToFinish;

    [Header("Game Objects")]
    [SerializeField] private GameObject _startCutScenePrefab;
    [SerializeField] private GameObject _catDiedScenePrefab;
    [SerializeField] private GameObject _finishGameScenePrefab;
    [SerializeField] private GameObject[] _levelPrefabs;

    [SerializeField] private GameObject _finishTrigger;
    [SerializeField] private GameObject _finishLevelPanel;
    [SerializeField] private GameObject _levelTitlePanel;

    private TextMeshProUGUI _txtLevel;
    private GameObject _currentLevelPrefab;
    private int _curLevel = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;

            _txtLevel = _levelTitlePanel.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Yandex.Instance.OnAuthStatusUpdated += OnAuthStatusUpdated;
        Yandex.Instance.OnAuthSuccess += OnAuthSuccess;

        //_currentSceneName = Progress.Instance.PlayerInfo.CurrentSceneName;
        string activeSceneName = SceneManager.GetActiveScene().name;

        LoadLevel(Progress.Instance.PlayerInfo.Level);        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    public int GetCurrentLevel()
    {
        return _curLevel;
    }

    public void LoadFirstLevel()
    {
        Progress.Instance.PlayerInfo.Score = 0;
        Progress.Instance.PlayerInfo.ChestCount = 0;
        Progress.Instance.PlayerInfo.Level = 0;
        Progress.Instance.PlayerInfo.Level = LevelManager.Instance.GetCurrentLevel();
        Progress.Instance.Save();

        _curLevel = 0;
        UpdateLevel(_curLevel);

        Player.Instance.SetPlayerStatus(PlayerStatus.InGame);

        GameManager.Instance.Resume();
    }

    public void FinishLevel()
    {
        TimerManager.Instance.StopTimer();
        _finishLevelPanel.SetActive(true);
        int chestCount = Progress.Instance.PlayerInfo.ChestCount;
        int scoreForChest = ChestManager.Instance.GetScoreForChest();
        int remainingTime = TimerManager.Instance.GetRemainingSeconds();
        _finishLevelPanel.GetComponent<FinishLevel>().UpdatePanel(chestCount, scoreForChest, remainingTime);
        //_audioSource.PlayOneShot(_audioClipFinishLevel);
        StartCoroutine(LoadNextLevel(SoundManager.Instance.GetLevelFinishMusicDuration()));
    }

    public void NextLevel()
    {
        if (IsLastLevel())
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            SceneManager.LoadScene("FinishGame");
        }
        else
        {
            _curLevel++;
            UpdateLevel(_curLevel);
        }
    }
    public void LoadLevel(int level)
    {
        _curLevel = level;

        GameManager.Instance.Resume();

        if (_txtLevel != null)
        {
            string strCurrentLevel = (Progress.Instance.PlayerInfo.Level + 1).ToString();//GetCurrentLevelNumber();

            if (Language.Instance.CurrentLanguage == "en")
            {
                _txtLevel.text = "Level " + strCurrentLevel;
            }
            else if (Language.Instance.CurrentLanguage == "ru")
            {
                _txtLevel.text = "Уровень " + strCurrentLevel;
            }
            else
            {
                _txtLevel.text = "Level " + strCurrentLevel;
            }
        }

        if (IsLastLevel())
        {
            _finishTrigger.GetComponent<SpriteRenderer>().sprite = _doorToFinish;
        }

        UpdateLevel(_curLevel);
    }

    public bool IsLastLevel()
    {
        return _curLevel == _levelPrefabs.Length - 1;
    }

    public void RestartLevel()
    {
        Progress.Instance.PlayerInfo.ChestCount = 0;
        UpdateLevel(_curLevel);
    }

    public void Resurrect()
    {
        GameManager.Instance.Resume();
        //SceneManager.LoadScene(Progress.Instance.PlayerInfo.Level);
        LoadLevel(Progress.Instance.PlayerInfo.Level);
    }

    public void LoadCatDiedScene()
    {
        if (_currentLevelPrefab != null)
        {
            Destroy(_currentLevelPrefab);
        }

        if (_catDiedScenePrefab != null)
        {
            Progress.Instance.PlayerInfo.ChestCount = 0;

            Instantiate(_catDiedScenePrefab);
            SpawnManager.Instance.Spawn();
        }
    }

    public void LoadFinishGameScene()
    {
        if (_currentLevelPrefab != null)
        {
            Destroy(_currentLevelPrefab);
        }

        if (_finishGameScenePrefab != null)
        {
            Progress.Instance.PlayerInfo.ChestCount = 0;

            Instantiate(_finishGameScenePrefab);
            SpawnManager.Instance.Spawn();
        }
    }

    private void UpdateLevel(int curLevel)
    {
        Player.Instance.ResetPhisic();

        if (_startCutScenePrefab != null)
        {
            Destroy(_startCutScenePrefab.gameObject);
        }

        if (_catDiedScenePrefab != null)
        {
            Destroy(_catDiedScenePrefab.gameObject);
        }

        if (_finishGameScenePrefab != null)
        {
            Destroy(_finishGameScenePrefab);
        }

        if (_currentLevelPrefab != null)
        {
            Destroy(_currentLevelPrefab);
        }

        for (int i = 0; i < _levelPrefabs.Length; i++)
        {
            if (i == curLevel)
            {
                _currentLevelPrefab = Instantiate(_levelPrefabs[i]);
                SpawnManager.Instance.Spawn();
            }
        }
    }

    private void OnAuthStatusUpdated()
    {
        if (Yandex.Instance.IsAuthorized)
        {
            Debug.Log("User is authorized");
        }
        else
        {
            Debug.Log("User is not authorized (guest mode)");
        }
    }

    private void OnAuthSuccess()
    {
        Debug.Log($"User authorized: {Yandex.Instance.PlayerName}");
    }

    IEnumerator LoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        //_currentSceneName = SceneManager.GetActiveScene().name;

        //Progress.Instance.PlayerInfo.CurrentSceneName = _currentSceneName;
        Progress.Instance.PlayerInfo.ChestCount = 0;

        if (!LevelManager.Instance.IsLastLevel())
        {
            Progress.Instance.PlayerInfo.Level = _curLevel;
            Progress.Instance.Save();
            NextLevel();
        }
        else
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            //SceneManager.LoadScene("FinishGame");
            LoadFinishGameScene();
        }

        //if (_currentSceneName != "Level" + (_sceneCount - 3))
        //{
        //    _levelBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //    Progress.Instance.PlayerInfo.LevelBuildIndex = _levelBuildIndex;
        //    Progress.Instance.Save();
        //    SceneManager.LoadScene(_levelBuildIndex);
        //}
        //else
        //{
        //    Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
        //    SceneManager.LoadScene("FinishGame");
        //}

        //_currentSceneName = SceneManager.GetActiveScene().name;

        Progress.Instance.Save();
    }
}
