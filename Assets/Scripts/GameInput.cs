using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private FixedJoystick _fixedJoystick;
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        Instance = this;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

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
}
