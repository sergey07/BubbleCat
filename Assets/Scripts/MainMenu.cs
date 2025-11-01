using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Yandex.Instance.InitYandexSDK();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Auth()
    {
        Yandex.Instance.RequestAuthorization();
    }
}
