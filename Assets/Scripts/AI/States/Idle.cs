using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public bool RemainActive = true;

    public override void OnEnter()
    {
        _currentPhase = StatePhase.ACTIVE;
    }

    public override void ActiveBehavior()
    {
        if(!RemainActive)
        {
            _currentPhase = StatePhase.EXITING;
        }
    }

    public override void OnExit()
    {
        _currentPhase = StatePhase.INACTIVE;
    }
}
