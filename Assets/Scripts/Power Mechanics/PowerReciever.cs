using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReciever : MonoBehaviour
{
    protected bool _isPowered;

    public void SetPowered(bool value)
    {
        _isPowered = value;
    }
}
