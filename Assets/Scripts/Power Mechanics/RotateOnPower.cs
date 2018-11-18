using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPower : MonoBehaviour
{
    public PowerSource power;
    public Rotatable Rotator;

    public Vector3 poweredRotation;
    public Vector3 unPoweredRotation;
    
    protected virtual void FixedUpdate()
    {
        bool powered = power.GetIsPowered();

        if(power.GetIsPowered())
        {
            Rotator.RotateObject(Quaternion.Euler(poweredRotation));
        }
        else
        {
            Rotator.RotateObject(Quaternion.Euler(unPoweredRotation));
        }
    }
}
