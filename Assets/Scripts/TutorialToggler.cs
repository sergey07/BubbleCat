using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToggler : MonoBehaviour
{
    [SerializeField] private GameObject _tutorPanel1;
    [SerializeField] private GameObject _tutorPanel2;

    private void Start()
    {
        _tutorPanel1.SetActive(true);
    }

    public void NextTutorial()
    {
        _tutorPanel1.SetActive(false);
        _tutorPanel2.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
