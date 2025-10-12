using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Auth()
    {
        Yandex.Instance.RequestAuthorization();
    }
}
