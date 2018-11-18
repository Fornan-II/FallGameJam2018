using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PressurePlate : PowerSource
{
    public float ExtraPowerTime = 0.3f;
    protected float _powerTimeRemaining = 0.0f;

    protected Animator _anim;

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        _anim.SetBool("IsDepressed", GetIsPowered());

        if(_powerTimeRemaining > 0.0f)
        {
            _powerTimeRemaining -= Time.fixedDeltaTime;
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            _powerTimeRemaining = ExtraPowerTime;
        }
    }

    public override bool GetIsPowered()
    {
        return _powerTimeRemaining > 0.0f;
    }
}
