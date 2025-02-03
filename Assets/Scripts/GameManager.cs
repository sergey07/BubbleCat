using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private string _currentSceneName = "StartScene";

    private void Awake()
    {
        Instance = this;
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
        GameProgress.currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("CatDied");

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
