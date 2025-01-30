using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
        Player.Instance.SetPlayerStatus(PlayerStatus.InGame);
    }

    public void LoadNextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "Level1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            SceneManager.LoadScene("FinishGame");
        }
    }

    public void LoadCatDiedScene()
    {
        GameProgress.currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("CatDied");
    }
}
