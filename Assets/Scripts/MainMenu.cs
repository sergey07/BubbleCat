using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    public void Auth()
    {
        Yandex.Instance.RequestAuthorization();
    }
}
