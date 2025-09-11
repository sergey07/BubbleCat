using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

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
    private GameObject _currentLevelGO;
    private int _curLevel = 0;

    private GameObject _startCutSceneGO;
    private GameObject _catDiedSceneGO;
    private GameObject _finishGameSceneGO;

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

    private void OnDestroy()
    {
        Yandex.Instance.OnAuthStatusUpdated -= OnAuthStatusUpdated;
        Yandex.Instance.OnAuthSuccess -= OnAuthSuccess;
    }

    public void Init()
    {
        Yandex.Instance.OnAuthStatusUpdated += OnAuthStatusUpdated;
        Yandex.Instance.OnAuthSuccess += OnAuthSuccess;

        Language.Instance.Init();

        Player.Instance.SetPlayerStatus(PlayerStatus.InGame);

        if (_curLevel == 0)
        {
            LoadFirstLevel();
        }

        LoadLevel(Progress.Instance.PlayerInfo.SavedLevel);
        GameManager.Instance.Resume();
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
        Progress.Instance.PlayerInfo.SavedLevel = 0;
        Progress.Instance.PlayerInfo.SavedLevel = _curLevel;
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
        SoundManager.Instance.PlayLevelFinishMusic();
        StartCoroutine(LoadNextLevel(SoundManager.Instance.GetLevelFinishMusicDuration()));
    }

    public void NextLevel()
    {
        if (IsLastLevel())
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            _finishGameSceneGO = Instantiate(_finishGameScenePrefab);
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
        string strCurrentLevel = (Progress.Instance.PlayerInfo.SavedLevel + 1).ToString();

        UpdateLevelTitle(strCurrentLevel);

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
        LoadLevel(Progress.Instance.PlayerInfo.SavedLevel);
    }

    public void LoadStartCutScene()
    {
        if (_currentLevelGO != null)
        {
            Destroy(_currentLevelGO);
        }

        if (_startCutScenePrefab != null)
        {
            ResetCamera();

            Progress.Instance.PlayerInfo.ChestCount = 0;

            _startCutSceneGO = Instantiate(_startCutScenePrefab);
            Player.Instance.Spawn(_startCutSceneGO);
        }
    }

    public void LoadCatDiedScene()
    {
        if (_currentLevelGO != null)
        {
            Destroy(_currentLevelGO);
        }

        if (_catDiedScenePrefab != null)
        {
            ResetCamera();

            Progress.Instance.PlayerInfo.ChestCount = 0;

            _catDiedSceneGO = Instantiate(_catDiedScenePrefab);
            Player.Instance.Spawn(_catDiedSceneGO);
        }
    }

    public void LoadFinishGameScene()
    {
        if (_currentLevelGO != null)
        {
            Destroy(_currentLevelGO);
        }

        if (_finishGameScenePrefab != null)
        {
            ResetCamera();

            Progress.Instance.PlayerInfo.ChestCount = 0;

            _finishGameSceneGO = Instantiate(_finishGameScenePrefab);
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            Player.Instance.Spawn(_finishGameSceneGO);
        }
    }

    private void UpdateLevel(int curLevel)
    {
        Player.Instance.ResetPhisic();

        if (_startCutSceneGO != null)
        {
            Destroy(_startCutSceneGO);
        }

        if (_catDiedSceneGO != null)
        {
            Destroy(_catDiedSceneGO);
        }

        if (_finishGameSceneGO != null)
        {
            Destroy(_finishGameSceneGO);
        }

        if (_currentLevelGO != null)
        {
            Destroy(_currentLevelGO);
        }

        for (int i = 0; i < _levelPrefabs.Length; i++)
        {
            if (i == curLevel)
            {
                _currentLevelGO = Instantiate(_levelPrefabs[i]);
                Player.Instance.SetPlayerStatus(PlayerStatus.InGame);
                Player.Instance.Spawn(_currentLevelGO);
            }
        }

        ChestManager.Instance.ResetChestCount();
        TimerManager.Instance.ResetTimer();
        ScoreManager.Instance.UpdateScore();
        GameManager.Instance.Resume();
    }

    private void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }

    private void UpdateLevelTitle(string strCurrentLevel)
    {
        if (_txtLevel == null)
        {
            return;
        }

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

        _finishLevelPanel.SetActive(false);
        Progress.Instance.PlayerInfo.ChestCount = 0;

        if (!IsLastLevel())
        {
            Progress.Instance.PlayerInfo.SavedLevel = _curLevel;
            Progress.Instance.Save();
            NextLevel();
        }
        else
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            LoadFinishGameScene();
        }

        Progress.Instance.Save();
        UpdateLevelTitle((_curLevel + 1).ToString());
        Player.Instance.Unfreeze();
    }
}
