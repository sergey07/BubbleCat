using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _txtChestCounter;

    private string _currentSceneName = "StartScene";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _txtChestCounter = GameObject.Find("txtChestCounter").GetComponent<TextMeshProUGUI>();
        _txtChestCounter.text = (GameProgress.levelChestCount + GameProgress.chestCount).ToString();
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
        string currentSceneName = SceneManager.GetActiveScene().name;
        GameProgress.chestCount += GameProgress.levelChestCount;
        GameProgress.levelChestCount = 0;

        if (_currentSceneName != "Level1")
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
        GameProgress.levelChestCount = 0;

        GameProgress.currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("CatDied");

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddReward(int reward)
    {
        GameProgress.levelChestCount += reward;
        _txtChestCounter.text = (GameProgress.levelChestCount + GameProgress.chestCount).ToString();
    }
}
