﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPower : PowerReciever
{
    public Rotatable Rotator;

    public Vector3 poweredRotation;
    public Vector3 unPoweredRotation;
    
    protected virtual void FixedUpdate()
    {
        if(_isPowered)
        {
            Rotator.RotateObject(Quaternion.Euler(poweredRotation));
        }
        else
        {
            Rotator.RotateObject(Quaternion.Euler(unPoweredRotation));
        }
    }
}
