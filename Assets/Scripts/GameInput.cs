using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private FixedJoystick _fixedJoystick;

    private PlayerInputActions _playerInputActions;
    private bool _isJoystickVisible = false;

    private void Awake()
    {
        Instance = this;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        ShowJoystick();
    }

    private void UpdateMaker()
    {
        // DisplayTime(Time.time);
        // CheckPauseButton();
    }

    public bool GetEscapeKey()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        if (_playerInputActions.Player.Move.ReadValue<Vector2>() != Vector2.zero &&
        _playerInputActions.Player.Move.ReadValue<Vector2>() != null)
        {
            // print(_playerInputActions.Player.Move.ReadValue<Vector2>());
            HideJoystick();
        }

        string currentSceneName = GameManager.Instance.GetCurrentSceneName();

        // Если схватил за джойстик, то он перезапишет значения - OnlyMeRus
        if (_fixedJoystick != null && (_fixedJoystick.Vertical != 0 || _fixedJoystick.Horizontal != 0))
        {
            inputVector = new Vector2(
                _fixedJoystick.Horizontal,
                _fixedJoystick.Vertical
            );
            inputVector.Normalize();
        }

        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    public bool IsJoystickVisible()
    {
        return _isJoystickVisible;
    }

    private void ShowJoystick()
    {
        _fixedJoystick.gameObject.SetActive(true);
        _isJoystickVisible = true;
    }

    private void HideJoystick()
    {
        _fixedJoystick.gameObject.SetActive(false);
        _isJoystickVisible = false;
    }
}
