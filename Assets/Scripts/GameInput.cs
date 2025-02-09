using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private FixedJoystick _fixedJoystick;

    //private PlayerInputActions _playerInputActions;

    private bool _isJoystickVisible = false;

    private void Awake()
    {
        if (Instance == null)
        {
            //transform.parent = null;
            //DontDestroyOnLoad(gameObject);

            Instance = this;

            //_playerInputActions = new PlayerInputActions();
            //_playerInputActions.Enable();

            ShowJoystick();
        }
        else
        {
            Destroy(gameObject);
        }
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
        //Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2

        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            inputVector.y = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = -1;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = 1;
        }

        if (inputVector != null && inputVector != Vector2.zero)
        {
            // print(_playerInputActions.Player.Move.ReadValue<Vector2>());
            HideJoystick();
        }

        string currentSceneName = GameManager.Instance.GetCurrentSceneName();

        // Если схватил за джойстик, то он перезапишет значения - OnlyMeRus
        if (_fixedJoystick != null && (_fixedJoystick.Vertical != 0 || _fixedJoystick.Horizontal != 0))
        {
            inputVector = new Vector2(_fixedJoystick.Horizontal, _fixedJoystick.Vertical);
        }

        inputVector.Normalize();

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
