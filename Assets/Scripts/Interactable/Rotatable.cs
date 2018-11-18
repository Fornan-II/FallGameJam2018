using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : Interactable
{
    public Vector3 rotationInterval;

    protected Quaternion _targetRotation;
    protected bool _isBeingRotated = false;

    protected virtual void FixedUpdate()
    {
        if(_isBeingRotated)
        {
            HandleRotation();
        }
    }

    public override void Interact(GameObject interacter)
    {
        Vector3 targetRot = transform.rotation.eulerAngles + rotationInterval;

        targetRot.x %= 360.0f;
        targetRot.y %= 360.0f;
        targetRot.z %= 360.0f;

        RotateObject(Quaternion.Euler(targetRot));
    }

    public virtual bool RotateObject(Quaternion rotation)
    {
        if(_isBeingRotated) { return false; }

        _isBeingRotated = true;
        _targetRotation = rotation;
        return true;

    }

    protected virtual void HandleRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 0.2f);
        if(Quaternion.Angle(transform.rotation, _targetRotation) < 1.0f)
        {
            transform.rotation = _targetRotation;
            _isBeingRotated = false;
        }
    }
}
