using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class State : MonoBehaviour
{
    public enum StatePhase
    {
        ENTERING,
        ACTIVE,
        EXITING,
        INACTIVE
    }
    protected StatePhase _currentPhase = StatePhase.ENTERING;
    public StatePhase CurrentPhase { get { return _currentPhase; } }

    public abstract void OnEnter();

    public abstract void ActiveBehavior();

    public abstract void OnExit();

    public virtual void EnterBehavior() { }

    public virtual void ExitBehavior() { }

    public virtual StateNode GetRequestedNextNode()
    {
        return null;
    }
}
