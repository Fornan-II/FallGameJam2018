using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PowerEvent : UnityEvent<bool> { }

public abstract class PowerSource : MonoBehaviour
{
    public PowerEvent PowerListeners;
    protected bool _isPowered;

    protected void SendPower()
    {
        PowerListeners.Invoke(_isPowered);
    }

#if UNITY_EDITOR
    public bool ShowPowerConnections = true;

    protected void OnDrawGizmos()
    {
        if (ShowPowerConnections)
        {
            for (int i = 0; i < PowerListeners.GetPersistentEventCount(); i++)
            {
                Object listener = PowerListeners.GetPersistentTarget(i);
                if (listener is MonoBehaviour)
                {
                    MonoBehaviour mb = listener as MonoBehaviour;
                    Debug.DrawLine(transform.position, mb.transform.position, Color.red);
                }
            }
        }
    }
#endif
}
