using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class TutorialToggler : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void LoadingApiReady();
#endif

    [SerializeField] private GameObject _tutorPanel1;
    [SerializeField] private GameObject _tutorPanel2;

    private void Start()
    {
        _tutorPanel1.SetActive(true);

#if !UNITY_EDITOR && UNITY_WEBGL
    LoadingApiReady();
#endif
    }

    public void NextTutorial()
    {
        _tutorPanel1.SetActive(false);
        _tutorPanel2.SetActive(true);
    }

    public void StartGame()
    {
        int levelBuildIndex = Progress.Instance.PlayerInfo.LevelBuildIndex;
        SceneManager.LoadScene(levelBuildIndex);
    }
}
