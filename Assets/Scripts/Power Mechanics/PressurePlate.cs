using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PressurePlate : PowerSource
{
    protected Animator _anim;

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        _anim.SetBool("IsDepressed", _isPowered);

        SendPower();

        _isPowered = false;
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            _isPowered = true;
        }
    }
}
