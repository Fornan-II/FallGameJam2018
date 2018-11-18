using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPower : MonoBehaviour
{
    public PowerSource power;
    public Rotatable Rotator;

    protected bool _oldPowerState;

    protected virtual void Start()
    {
        _oldPowerState = power.GetIsPowered();
    }

    protected virtual void FixedUpdate()
    {
        bool powered = power.GetIsPowered();

        if(_oldPowerState != powered)
        {
            Vector3 targetRot = transform.rotation.eulerAngles + Rotator.rotationInterval;

            if (!powered)
            {
                targetRot *= -1;
            }

            targetRot.x %= 360.0f;
            targetRot.y %= 360.0f;
            targetRot.z %= 360.0f;

            Rotator.RotateObject(Quaternion.Euler(targetRot));
        }

        _oldPowerState = powered;
    }
}
