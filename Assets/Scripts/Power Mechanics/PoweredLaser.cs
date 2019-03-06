using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredLaser : PowerReciever
{
    public LaserSource laser;
    public bool ActiveByDefault = true;

    protected virtual void FixedUpdate()
    {
        if(ActiveByDefault)
        {
            laser.LaserActive = !_isPowered;
        }
        else
        {
            laser.LaserActive = _isPowered;
        }
    }
}
