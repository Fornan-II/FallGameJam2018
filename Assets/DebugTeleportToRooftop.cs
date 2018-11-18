using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleportToRooftop : MonoBehaviour
{
    public Vector3[] RoofTopPositions;

    private void Update()
    {
        if(RoofTopPositions.Length < 9) { return; }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = RoofTopPositions[0];
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = RoofTopPositions[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = RoofTopPositions[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = RoofTopPositions[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            transform.position = RoofTopPositions[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            transform.position = RoofTopPositions[5];
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            transform.position = RoofTopPositions[6];
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            transform.position = RoofTopPositions[7];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.position = RoofTopPositions[8];
        }
    }
}
