using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public bool RemainActive = true;

    public override void OnEnter()
    {
        _currentPhase = StatePhase.ACTIVE;
        Debug.Log("OnEnter");
    }

    public override void ActiveBehavior()
    {
        if(!RemainActive)
        {
            _currentPhase = StatePhase.EXITING;
        }

        Debug.Log("Idling");
    }

    public override void OnExit()
    {
        _currentPhase = StatePhase.INACTIVE;
        Debug.Log("OnExit");
    }
}
