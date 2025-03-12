using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private float _deltaPos = 300f;

    private bool _isJoystickVisible = false;
    private Vector2 _oldTouchPos = new Vector2(0, 0);

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

            // Placing joystick on touch position
            Vector2 touchPos = Input.GetTouch(0).position;

            if (CheckDeltaPosition(touchPos))
            {
                _oldTouchPos = touchPos;
                _fixedJoystick.transform.position = new Vector3(touchPos.x, touchPos.y, 0);
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

    // Is this delta enough for moving the joystick? 
    private bool CheckDeltaPosition(Vector2 touchPos)
    {
        float xMin = _oldTouchPos.x - _deltaPos;
        float xMax = _oldTouchPos.x + _deltaPos;
        float yMin = _oldTouchPos.y - _deltaPos;
        float yMax = _oldTouchPos.y + _deltaPos;

        return touchPos.x < xMin || touchPos.x > xMax || touchPos.y < yMin || touchPos.y > yMax;
    }
}
