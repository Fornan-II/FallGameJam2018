using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredLaser : MonoBehaviour
{
    public PowerSource source;
    public LaserSource laser;
    public bool ActiveByDefault = true;

    protected virtual void FixedUpdate()
    {
        if(ActiveByDefault)
        {
            laser.LaserActive = !source.GetIsPowered();
        }
        else
        {
            laser.LaserActive = source.GetIsPowered();
        }
    }

}
