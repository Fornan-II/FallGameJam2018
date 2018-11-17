using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : PowerSource
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

    public virtual void RecieveLaser()
    {
        _powerTimeRemaining = ExtraPowerTime;
    }

    public override bool GetIsPowered()
    {
        return _powerTimeRemaining > 0.0f;
    }
}
