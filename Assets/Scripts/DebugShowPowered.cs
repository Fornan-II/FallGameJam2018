using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShowPowered : MonoBehaviour
{
    public PowerSource source;
    public bool isPowered;

    private void Update()
    {
        if(source)
        {
            isPowered = source.GetIsPowered();
        }
        else
        {
            isPowered = false;
        }
        
    }
}
