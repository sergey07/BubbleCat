using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private GameObject _pausePanel;

    [Header("Difficulty Configuration")]
    [SerializeField] private float _slowSpeed = 1.0f;
    [SerializeField] private float _middleSpeed = 1.5f;
    [SerializeField] private float _fastSpeed = 2.0f;

    private float _currentSpeed = 1.0f;
    private int _difficultyLvl;
    private bool _isZoom ;
    private int _joystickPosition;

    private PausePanel _ppComponent;

    private void Start()
    {
        _ppComponent = _pausePanel.GetComponent<PausePanel>();
        Init();
    }

    public void Init()
    {
        _difficultyLvl = Progress.Instance.PlayerInfo.DifficultyLvl;
        UpdateSpeed(_difficultyLvl);
        _isZoom = Progress.Instance.PlayerInfo.IsZoom;
        UpdateCamera(_isZoom);
        _joystickPosition = Progress.Instance.PlayerInfo.JoystickPos;
        UpdateJoystickPosition(_joystickPosition);
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
        bool isMute = !SoundManager.Instance.IsMute();
        SoundManager.Instance.Mute(isMute);

        Progress.Instance.PlayerInfo.IsSoundOn = !isMute;
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif

        _ppComponent.UpdateSoundButton(!SoundManager.Instance.IsMute());
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
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
    }

    public void ChangeScale()
    {
        _isZoom = !_isZoom;
        Progress.Instance.PlayerInfo.IsZoom = _isZoom;
        UpdateCamera(_isZoom);
        _ppComponent.UpdateZoomButton(_isZoom);
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
    }

    public void ChangeJoystickPosition()
    {
        _joystickPosition = _joystickPosition == 1 ? 2 : 1;
        Progress.Instance.PlayerInfo.JoystickPos = _joystickPosition;
        UpdateJoystickPosition(_joystickPosition);
        _ppComponent.UpdateJoystickPositionButton(_joystickPosition, GameInput.Instance.IsJoystickVisible());
#if UNITY_WEBGL
        Progress.Instance.Save();
#endif
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
        if (isZoom)
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

    private void UpdateJoystickPosition(int joystickPosition)
    {
        if (joystickPosition == 1)
        {
            GameInput.Instance.SetJoystickLeft();
        }
        else if (joystickPosition == 2)
        {
            GameInput.Instance.SetJoystickRight();
        }
    }

    private void UpdatePausePanelButtons()
    {
        _ppComponent.UpdateSoundButton(Progress.Instance.PlayerInfo.IsSoundOn);
        _ppComponent.UpdateDifficultyButton(_difficultyLvl);
        _ppComponent.UpdateZoomButton(_isZoom);
        _ppComponent.UpdateJoystickPositionButton(_joystickPosition, GameInput.Instance.IsJoystickVisible());
    }
}
