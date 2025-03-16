using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _joystickOffsetX = 80.0f;
    [SerializeField] private float _joystickPosY = 80.0f;
    
    private Vector3 _joystickPos;
    private bool _isJoystickVisible = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!_isJoystickVisible)
            {
                ShowJoystick();
            }
        }
    }

    public bool GetEscapeKey()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y = 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = 1;
        }

        string currentSceneName = GameManager.Instance.GetCurrentSceneName();

        // Если схватил за джойстик, то он перезапишет значения - OnlyMeRus
        if (_joystick != null && (_joystick.Vertical != 0 || _joystick.Horizontal != 0))
        {
            inputVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        }

        inputVector.Normalize();

        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    public void SetJoystickRight()
    {
        _joystickPos = new Vector3(-_joystickOffsetX, _joystickPosY, 0);

        RectTransform rt = _joystick.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(1.0f, 0.0f);
        rt.anchorMax = new Vector2(1.0f, 0.0f);
        rt.pivot = new Vector2(1.0f, 0.0f);
        rt.anchoredPosition = _joystickPos;
    }

    public void SetJoystickLeft()
    {
        _joystickPos = new Vector3(_joystickOffsetX, _joystickPosY, 0);

        RectTransform rt = _joystick.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.0f, 0.0f);
        rt.anchorMax = new Vector2(0.0f, 0.0f);
        rt.pivot = new Vector2(0.0f, 0.0f);
        rt.anchoredPosition = _joystickPos;
    }

    public bool IsJoystickVisible()
    {
        return _isJoystickVisible;
    }

    private void ShowJoystick()
    {
        _joystick.gameObject.SetActive(true);
        _isJoystickVisible = true;
    }

    private void HideJoystick()
    {
        _joystick.gameObject.SetActive(false);
        _isJoystickVisible = false;
    }
}
