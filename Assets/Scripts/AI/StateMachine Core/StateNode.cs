using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StateNode", menuName = "StateNode")]
public class StateNode : ScriptableObject
{
    public string StateName;
    public StateNode NextNode;
}
