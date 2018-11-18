using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pawn ControlledPawn;

    public string[] Inputs;
    private enum Action
    {
        JOYSTICK_HORIZONTAL,
        JOYSTICK_VERTICAL,
        JUMP,
        INTERACT,
        ROTATE_CAMERA_ANGLE_LEFT,
        ROTATE_CAMERA_ANGLE_RIGHT
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    #region Input
    protected virtual void HandleInput()
    {
        float horizontal = Input.GetAxis(Inputs[(int)Action.JOYSTICK_HORIZONTAL]);
        float vertical = Input.GetAxis(Inputs[(int)Action.JOYSTICK_VERTICAL]);
        PassJoystick(new Vector2(horizontal, vertical));

        PassJump(Input.GetButton(Inputs[(int)Action.JUMP]));
        PassInteract(Input.GetButtonDown(Inputs[(int)Action.INTERACT]));
        PassRotateCameraAngleLeft(Input.GetButtonDown(Inputs[(int)Action.ROTATE_CAMERA_ANGLE_LEFT]));
        PassRotateCameraAngleRight(Input.GetButtonDown(Inputs[(int)Action.ROTATE_CAMERA_ANGLE_RIGHT]));
    }

    protected virtual void PassJoystick(Vector2 input)
    {
        if(ControlledPawn)
        {
            ControlledPawn.HandleMovement(input);
        }
    }

    protected virtual void PassJump(bool value)
    {
        if (ControlledPawn)
        {
            ControlledPawn.HandleJump(value);
        }
    }

    protected virtual void PassInteract(bool value)
    {
        if (ControlledPawn)
        {
            ControlledPawn.HandleInteract(value);
        }
    }

    protected virtual void PassRotateCameraAngleLeft(bool value)
    {
        if (ControlledPawn)
        {
            ControlledPawn.HandleRotateCameraAngleLeft(value);
        }
    }

    protected virtual void PassRotateCameraAngleRight(bool value)
    {
        if(ControlledPawn)
        {
            ControlledPawn.HandleRotateCameraAngleRight(value);
        }
    }
    #endregion
}
