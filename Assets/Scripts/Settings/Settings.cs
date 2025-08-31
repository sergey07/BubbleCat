using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private GameObject _pausePanel;

    private int _joystickPosition;

    private PausePanel _ppComponent;

    private void Start()
    {
        _ppComponent = _pausePanel.GetComponent<PausePanel>();
        Init();
    }

    public void Init()
    {
        _joystickPosition = Progress.Instance.PlayerInfo.JoystickPos;
        UpdateJoystickPosition(_joystickPosition);
    }

    public void ToggleMenu()
    {
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        
        if (_pausePanel.activeSelf)
        {
            GameManager.Instance.Pause();
        }
        else
        {
            GameManager.Instance.Resume();
        }

        UpdatePausePanelButtons();
    }

    public void Restart()
    {
        GameManager.Instance.Resume();
        LevelManager.Instance.RestartLevel();
    }

    public void SwitchSound()
    {
        bool isMute = !SoundManager.Instance.IsMute();
        SoundManager.Instance.Mute(isMute);

        Progress.Instance.PlayerInfo.IsSoundOn = !isMute;
#if !UNITY_EDITOR && UNITY_WEBGL
        Progress.Instance.Save();
#endif

        _ppComponent.UpdateSoundButton(!SoundManager.Instance.IsMute());
    }

    public void ChangeJoystickPosition()
    {
        _joystickPosition = _joystickPosition == 1 ? 2 : 1;
        Progress.Instance.PlayerInfo.JoystickPos = _joystickPosition;
        UpdateJoystickPosition(_joystickPosition);
        _ppComponent.UpdateJoystickPositionButton(_joystickPosition, GameInput.Instance.IsJoystickVisible());
#if !UNITY_EDITOR && UNITY_WEBGL
        Progress.Instance.Save();
#endif
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
        _ppComponent.UpdateJoystickPositionButton(_joystickPosition, GameInput.Instance.IsJoystickVisible());
    }
}
