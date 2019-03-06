using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShowPowered : PowerReciever
{
    public PowerSource source;
    public bool isPowered;

    private void Update()
    {
        isPowered = _isPowered;
    }
}
