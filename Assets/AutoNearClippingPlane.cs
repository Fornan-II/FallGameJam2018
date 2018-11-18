using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AutoNearClippingPlane : MonoBehaviour
{
    public Transform FocusedObject;

    protected Camera _cam;

    protected virtual void Start()
    {
        _cam = GetComponent<Camera>();
    }

    protected virtual void Update()
    {
        Ray cameraRay = new Ray(FocusedObject.transform.position, transform.position - FocusedObject.position);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction);
        RaycastHit hit;
        if(Physics.Raycast(cameraRay, out hit, Mathf.Abs(transform.position.z) / 2.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {

        }
        else
        {

        }
    }
}
