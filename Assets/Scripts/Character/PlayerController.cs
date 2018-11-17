using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public Pawn ControlledPawn;

    public string[] Inputs;
    private enum Action
    {
        JOYSTICK_HORIZONTAL,
        JOYSTICK_VERTICAL,
        JUMP,
        INTERACT,
        TOGGLE_SPRINT
    }
    //0: Joystick horizontal axis
    //1: Joystick vertical axis
    //2: Jump button
    //3: Interact
    //4: Toggle sprint

    protected virtual void Update()
    {
        
    }

    protected virtual void HandleInput()
    {
        //Inputs[Action.JOYSTICK_HORIZONTAL]
    }

    protected virtual void PassJoystick(Vector2 value)
    {

    }
}
