using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : PowerSource
{
    public float ExtraPowerTime = 0.3f;
    protected float _powerTimeRemaining = 0.0f;

    protected virtual void FixedUpdate()
    {
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
