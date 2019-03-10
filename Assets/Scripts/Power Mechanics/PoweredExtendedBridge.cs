using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredExtendedBridge : PowerReciever
{
    public Transform _bridge;

    public bool letExtend = true;
    public bool letRetract = true;
    public bool oscillateWhenPowered = false;
    public bool invertDirection = false;

    public Vector3 _retractedPosition;
    public Vector3 _extendedPosition;
    public float timeToExtend = 5.0f;

    [SerializeField]protected float extensionTime = 0.0f;

    protected virtual void FixedUpdate()
    {
        if (_isPowered && letExtend)
        {
            if(invertDirection)
            {
                Retract();
            }
            else
            {
                Extend();
            }
        }
        else if (letRetract)
        {
            if (invertDirection)
            {
                Extend();
            }
            else
            {
                Retract();
            }
        }
    }

    public virtual void ResetBridge()
    {
        extensionTime = 0.0f;
        _bridge.localPosition = _retractedPosition;
    }

    protected virtual void Extend()
    {
        _bridge.localPosition = Vector3.Lerp(_retractedPosition, _extendedPosition, extensionTime / timeToExtend);

        if (extensionTime < timeToExtend)
        {
            extensionTime += Time.fixedDeltaTime;
        }
        else if (extensionTime >= timeToExtend)
        {
            extensionTime = timeToExtend;
            if(oscillateWhenPowered && _isPowered)
            {
                invertDirection = !invertDirection;
            }
        }
    }

    protected virtual void Retract()
    {
        _bridge.localPosition = Vector3.Lerp(_retractedPosition, _extendedPosition, extensionTime / timeToExtend);

        if (extensionTime > 0.0f)
        {
            extensionTime -= Time.fixedDeltaTime;
        }
        else if (extensionTime < 0.0f)
        {
            extensionTime = 0.0f;
            if (oscillateWhenPowered && _isPowered)
            {
                invertDirection = !invertDirection;
            }
        }
    }
}
