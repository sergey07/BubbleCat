using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _chestCounterPanel;
    [SerializeField] private TextMeshProUGUI _txtChestCounter;

    private int _sceneCount;
    private string _currentSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            //transform.parent = null;
            //DontDestroyOnLoad(gameObject);
            Instance = this;
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

    public void LoadNextLevel()
    {
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
}
