using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

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

    //public string GetCurrentSceneName()
    //{
    //    return _currentSceneName;
    //}

    //public void LoadFirstLevel()
    //{
    //    Resume();

    //    Progress.Instance.PlayerInfo.Score = 0;
    //    Progress.Instance.PlayerInfo.ChestCount = 0;
    //    Progress.Instance.PlayerInfo.Level = 0;
    //    Progress.Instance.PlayerInfo.Level = LevelManager.Instance.GetCurrentLevel();
    //    Progress.Instance.Save();

    //    LevelManager.Instance.FirstLevel();
    //    Player.Instance.SetPlayerStatus(PlayerStatus.InGame);

    //    _currentSceneName = SceneManager.GetActiveScene().name;
    //}

    //public void FinishLevel()
    //{
    //    TimerManager.Instance.StopTimer();
    //    _finishLevelPanel.SetActive(true);
    //    int chestCount = Progress.Instance.PlayerInfo.ChestCount;
    //    int scoreForChest = ChestManager.Instance.GetScoreForChest();
    //    int remainingTime = TimerManager.Instance.GetRemainingSeconds();
    //    _finishLevelPanel.GetComponent<FinishLevel>().UpdatePanel(chestCount, scoreForChest, remainingTime);
    //    _audioSource.PlayOneShot(_audioClipFinishLevel);
    //    StartCoroutine(LoadNextLevel(_audioClipFinishLevel.length));
    //}

    //public void LoadCatDiedScene()
    //{
    //    Progress.Instance.PlayerInfo.ChestCount = 0;

    //    Progress.Instance.PlayerInfo.CurrentSceneName = SceneManager.GetActiveScene().name;
    //    SceneManager.LoadScene("CatDied");

    //    //_currentSceneName = SceneManager.GetActiveScene().name;
    //}

    //public void RestartScene()
    //{
    //    Progress.Instance.PlayerInfo.ChestCount = 0;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //public void Resurrect()
    //{
    //    Resume();
    //    //SceneManager.LoadScene(Progress.Instance.PlayerInfo.CurrentSceneName);
    //    SceneManager.LoadScene(Progress.Instance.PlayerInfo.Level);
    //}

    //IEnumerator LoadNextLevel(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    //_currentSceneName = SceneManager.GetActiveScene().name;

    //    //Progress.Instance.PlayerInfo.CurrentSceneName = _currentSceneName;
    //    Progress.Instance.PlayerInfo.ChestCount = 0;

    //    if (!LevelManager.Instance.IsLastLevel())
    //    {
    //        Progress.Instance.PlayerInfo.Level = LevelManager.Instance.GetCurrentLevel();
    //        Progress.Instance.Save();
    //        LevelManager.Instance.NextLevel();
    //    }
    //    else
    //    {
    //        Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
    //        SceneManager.LoadScene("FinishGame");
    //    }

    //    //if (_currentSceneName != "Level" + (_sceneCount - 3))
    //    //{
    //    //    _levelBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
    //    //    Progress.Instance.PlayerInfo.LevelBuildIndex = _levelBuildIndex;
    //    //    Progress.Instance.Save();
    //    //    SceneManager.LoadScene(_levelBuildIndex);
    //    //}
    //    //else
    //    //{
    //    //    Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
    //    //    SceneManager.LoadScene("FinishGame");
    //    //}

    //    //_currentSceneName = SceneManager.GetActiveScene().name;

    //    Progress.Instance.Save();
    //}

    //private int GetCurrentLevelNumber()
    //{
    //    return SceneManager.GetActiveScene().buildIndex + 1;
    //}

    

}
