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
        TOGGLE_SPRINT,
        SWITCH_CAMERA_ANGLE
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
        PassSprint(Input.GetButtonDown(Inputs[(int)Action.TOGGLE_SPRINT]));
        PassSwitchCameraAngle(Input.GetButtonDown(Inputs[(int)Action.SWITCH_CAMERA_ANGLE]));
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

    protected virtual void PassSprint(bool value)
    {
        if (ControlledPawn)
        {
            ControlledPawn.HandleSprint(value);
        }
    }

    protected virtual void PassSwitchCameraAngle(bool value)
    {
        if(ControlledPawn)
        {
            ControlledPawn.HandleSwitchCameraAngle(value);
        }
    }
    #endregion
}
