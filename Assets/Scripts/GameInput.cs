using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Если схватил за джойстик, то он перезапишет значения - OnlyMeRus
        if ((fixedJoystick.Vertical != 0) || fixedJoystick.Horizontal != 0){
        inputVector = new Vector2(
            fixedJoystick.Horizontal,
            fixedJoystick.Vertical
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
