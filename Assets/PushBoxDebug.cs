using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxDebug : MonoBehaviour
{
    public Vector3 pushDir = Vector3.zero;
    public bool Push = false;

	void Update ()
    {
		if(Push)
        {
            PushableBox pb = GetComponent<PushableBox>();
            if(pb)
            {
                Debug.DrawRay(transform.position, pushDir, Color.red, 1.0f);
                pb.PushBox(pushDir);
            }

            Push = false;
        }
	}
}
