using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredExtendedBridge : MonoBehaviour
{
    public PowerSource source;
    public Transform _bridge;

    public Vector3 _retractedPosition;
    public Vector3 _extendedPosition;
    public float timeToExtend = 5.0f;

    protected float extensionTime = 0.0f;

    protected virtual void FixedUpdate()
    {
        if(source.GetIsPowered())
        {
            _bridge.position = Vector3.Lerp(_retractedPosition, _extendedPosition, extensionTime / timeToExtend);
            
            if(extensionTime < timeToExtend)
            {
                extensionTime += Time.fixedDeltaTime;
            }
            else if(extensionTime >= timeToExtend)
            {
                extensionTime = timeToExtend;
            }
        }
        else
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
}
