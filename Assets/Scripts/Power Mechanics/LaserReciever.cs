using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : PowerSource
{
    protected virtual void FixedUpdate()
    {
        SendPower();

        _isPowered = false;
    }

    public virtual void RecieveLaser()
    {
        _isPowered = true;
    }
}
