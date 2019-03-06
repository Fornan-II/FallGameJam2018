using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDeployer : PowerReciever
{
    public GameObject BoxPrefab;

    protected GameObject _currentlySpawnedBox;

    protected bool _wasPowered = false;

    protected virtual void FixedUpdate()
    {
        if(_isPowered && _isPowered != _wasPowered)
        {
            OnBecomePowered();
        }

        _wasPowered = _isPowered;
    }

    protected virtual void OnBecomePowered()
    {
        if(_currentlySpawnedBox)
        {
            DestroyBox();
        }

        _currentlySpawnedBox = Instantiate(BoxPrefab, transform.position, transform.rotation);
    }

    public virtual void DestroyBox()
    {
        Destroy(_currentlySpawnedBox);
        _currentlySpawnedBox = null;
    }
}
