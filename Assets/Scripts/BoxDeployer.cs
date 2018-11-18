using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDeployer : MonoBehaviour
{
    public GameObject BoxPrefab;
    public PowerSource TrackedPowerSource;

    protected GameObject _currentlySpawnedBox;

    protected bool _wasPowered = false;

    protected virtual void FixedUpdate()
    {
        bool powerState = TrackedPowerSource.GetIsPowered();
        if(powerState && powerState != _wasPowered)
        {
            OnBecomePowered();
        }

        _wasPowered = powerState;
    }

    protected virtual void OnBecomePowered()
    {
        if(_currentlySpawnedBox)
        {
            DestroyBox();
        }

        _currentlySpawnedBox = Instantiate(BoxPrefab, transform.position, transform.rotation);
    }

    protected virtual void DestroyBox()
    {
        Destroy(_currentlySpawnedBox);
    }
}
