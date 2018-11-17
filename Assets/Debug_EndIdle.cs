using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class Debug_EndIdle : MonoBehaviour {

    public bool EndIdle = false;

    StateMachine _sm;

	// Use this for initialization
	void Start () {
        _sm = GetComponent<StateMachine>();
	}
	
	// Update is called once per frame
	void Update () {
        if(EndIdle)
        {
            _sm.EndIdle();
            EndIdle = false;
        }
	}
}
