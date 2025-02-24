using UnityEngine;
using TMPro;

public class PausePanel : MonoBehaviour
{
    [Header("Required Components")]
    //[SerializeField] private MusicToggler _musicToggler;
    [SerializeField] private TextMeshProUGUI _soundBtnText;
    [SerializeField] private TextMeshProUGUI _zoomBtnText;
    [SerializeField] private TextMeshProUGUI _difficultyBtnText;

    [Header("Difficulty Configuration")]
    [SerializeField] private float _slowSpeed = 1.0f;
    [SerializeField] private float _middleSpeed = 1.5f;
    [SerializeField] private float _fastSpeed = 2.0f;

    private float _currentSpeed = 1.0f;
    private int _difficultyLvl = 1;

    
    private bool _isZoomBig = true;

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 0.0f : _currentSpeed;
    }

    public void Restart()
    {
        Time.timeScale = _currentSpeed;
        GameManager.Instance.RestartScene();
    }

    public void SwitchSound()
    {
        AudioListener.pause = !AudioListener.pause;

        if (AudioListener.pause)
        {
            _soundBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Вкл. звук" : "Sound On";
        }
        else
        {
            _soundBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Выкл. звук" : "Sound Off";
        }
    }

    public void ChangeDifficulty()
    {
        _difficultyLvl++;

        if (_difficultyLvl > 3)
        {
            _difficultyLvl = 1;
        }

        switch (_difficultyLvl)
        {
            case 1:
                _currentSpeed = _slowSpeed;
                _difficultyBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Легко" : "Easy";
                break;
            case 2:
                _currentSpeed = _middleSpeed;
                _difficultyBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Норально" : "Medium";
                break;
            case 3:
                _currentSpeed = _fastSpeed;
                _difficultyBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Сложно" : "Hard";
                break;
        }
    }
    
    public void ChangeScale()
    {
        if (_isZoomBig)
        {
            // Set the size of the viewing volume you'd like the orthographic Camera to pick up
            Camera.main.orthographicSize = 6.0f;
            _isZoomBig = false;
            _zoomBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Дальше" : "Zoom Out";
        }
        else
        {
            // Set the size of the viewing volume you'd like the orthographic Camera to pick up
            Camera.main.orthographicSize = 10.0f;
            _isZoomBig = true;
            _zoomBtnText.text = Language.Instance.CurrentLanguage == "ru" ? "Ближе" : "Zoom In";
        }
    }
}
