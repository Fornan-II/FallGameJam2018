using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateNode CurrentNode;
    protected State _activeState;
    protected State.StatePhase _oldStatePhase = State.StatePhase.INACTIVE;

    protected virtual void FixedUpdate()
    {
        StateMachineProcessState();
    }

    protected virtual void StateMachineProcessState()
    {
        if(!_activeState && CurrentNode)
        {
            _activeState = ScriptableObject.CreateInstance(CurrentNode.StateName) as State;
        }

        if(_activeState)
        {
            State.StatePhase phaseOnStateProcessing = _activeState.CurrentPhase;

            switch (_activeState.CurrentPhase)
            {
                case State.StatePhase.ENTERING:
                    {
                        if (_oldStatePhase != _activeState.CurrentPhase)
                        {
                            _activeState.OnEnter();
                        }
                        else
                        {
                            _activeState.EnterBehavior();
                        }
                        break;
                    }
                case State.StatePhase.ACTIVE:
                    {
                        _activeState.ActiveBehavior();
                        break;
                    }
                case State.StatePhase.EXITING:
                    {
                        if (_oldStatePhase != _activeState.CurrentPhase)
                        {
                            _activeState.OnExit();
                        }
                        else
                        {
                            _activeState.ExitBehavior();
                        }
                        break;
                    }
                case State.StatePhase.INACTIVE:
                    {
                        if (_oldStatePhase != _activeState.CurrentPhase)
                        {
                            StateNode requestedNextNode = _activeState.GetRequestedNextNode();
                            if(requestedNextNode)
                            {
                                CurrentNode = requestedNextNode;
                            }
                            else
                            {
                                CurrentNode = CurrentNode.NextNode;
                            }
                        }
                        else
                        {
                            CurrentNode = CurrentNode.NextNode;
                        }

                        _activeState = null;
                        break;
                    }
            }

            _oldStatePhase = phaseOnStateProcessing;
        }
    }

    public void EndIdle()
    {
        if(_activeState is Idle)
        {
            (_activeState as Idle).RemainActive = false;
        }
    }
}
