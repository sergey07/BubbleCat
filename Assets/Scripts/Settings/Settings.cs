using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private GameObject _pausePanel;

    [Header("Difficulty Configuration")]
    [SerializeField] private float _slowSpeed = 1.0f;
    [SerializeField] private float _middleSpeed = 1.5f;
    [SerializeField] private float _fastSpeed = 2.0f;

    private float _currentSpeed = 1.0f;
    private int _difficultyLvl = 1;
    private bool _isZoom = false;

    private PausePanel _ppComponent;

    private void Start()
    {
        _ppComponent = _pausePanel.GetComponent<PausePanel>();
        Init();
    }

    public void Init()
    {
        AudioListener.pause = !Progress.Instance.PlayerInfo.IsSoundOn;;
        _difficultyLvl = Progress.Instance.PlayerInfo.DifficultyLvl;
        UpdateSpeed(_difficultyLvl);
        _isZoom = Progress.Instance.PlayerInfo.IsZoom;
        UpdateCamera(_isZoom);
    }

    public void ToggleMenu()
    {
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        Time.timeScale = _pausePanel.activeSelf ? 0.0f : _currentSpeed;
        UpdatePausePanelButtons();
    }

    public void Restart()
    {
        Time.timeScale = _currentSpeed;
        GameManager.Instance.RestartScene();
    }

    public void SwitchSound()
    {
        AudioListener.pause = !AudioListener.pause;
        Progress.Instance.PlayerInfo.IsSoundOn = !AudioListener.pause;
        _ppComponent.UpdateSoundButton(Progress.Instance.PlayerInfo.IsSoundOn);
    }

    public void ChangeDifficulty()
    {
        _difficultyLvl++;

        if (_difficultyLvl > 3)
        {
            _difficultyLvl = 1;
        }

        Progress.Instance.PlayerInfo.DifficultyLvl = _difficultyLvl;
        UpdateSpeed(_difficultyLvl);
        _ppComponent.UpdateDifficultyButton(_difficultyLvl);
    }

    public void ChangeScale()
    {
        _isZoom = !_isZoom;
        Progress.Instance.PlayerInfo.IsZoom = _isZoom;
        UpdateCamera(_isZoom);
        _ppComponent.UpdateZoomButton(_isZoom);
    }

    private void UpdateSpeed(int difficultyLvl)
    {
        switch (difficultyLvl)
        {
            case 1:
                _currentSpeed = _slowSpeed;
                break;
            case 2:
                _currentSpeed = _middleSpeed;
                break;
            case 3:
                _currentSpeed = _fastSpeed;
                break;
        }
    }

    private void UpdateCamera(bool isZoom)
    {
        if (_isZoom)
        {
            // Set the size of the viewing volume you'd like the orthographic Camera to pick up
            Camera.main.orthographicSize = 6.0f;
        }
        else
        {
            // Set the size of the viewing volume you'd like the orthographic Camera to pick up
            Camera.main.orthographicSize = 10.0f;
        }
    }

    private void UpdatePausePanelButtons()
    {
        _ppComponent.UpdateSoundButton(Progress.Instance.PlayerInfo.IsSoundOn);
        _ppComponent.UpdateDifficultyButton(_difficultyLvl);
        _ppComponent.UpdateZoomButton(_isZoom);
    }
}
