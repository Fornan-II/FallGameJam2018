﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredExtendedBridge : PowerReciever
{
    public Transform _bridge;

    public bool letExtend = true;
    public bool letRetract = true;

    public Vector3 _retractedPosition;
    public Vector3 _extendedPosition;
    public float timeToExtend = 5.0f;

    protected float extensionTime = 0.0f;

    protected virtual void FixedUpdate()
    {
        if (_isPowered && letExtend)
        {
            _bridge.position = Vector3.Lerp(_retractedPosition, _extendedPosition, extensionTime / timeToExtend);

            if (extensionTime < timeToExtend)
            {
                extensionTime += Time.fixedDeltaTime;
            }
            else if (extensionTime >= timeToExtend)
            {
                extensionTime = timeToExtend;
            }
        }
        else if (letRetract)
        {
            _bridge.position = Vector3.Lerp(_retractedPosition, _extendedPosition, extensionTime / timeToExtend);

            if (extensionTime > 0.0f)
            {
                extensionTime -= Time.fixedDeltaTime;
            }
            else if (extensionTime < 0.0f)
            {
                extensionTime = 0.0f;
            }
        }
    }

    public virtual void ResetBridge()
    {

    }
}
