using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSource : MonoBehaviour
{
    public Transform LaserSourceTransform;
    public bool LaserActive = true;
    protected LineRenderer _lr;

    protected virtual void Start()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = 2;
        UpdateLaserState();
    }

    protected virtual void FixedUpdate()
    {
        UpdateLaserState();

        if(LaserActive)
        {
            Ray laserRayCast = new Ray(LaserSourceTransform.position, LaserSourceTransform.forward);
            RaycastHit hit;
            _lr.SetPosition(0, LaserSourceTransform.position);
            if (Physics.Raycast(laserRayCast, out hit, 100.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                _lr.SetPosition(1, hit.point);
                LaserReciever reciever = hit.transform.GetComponent<LaserReciever>();
                if(reciever)
                {
                    reciever.RecieveLaser();
                }
            }
            else
            {
                _lr.SetPosition(1, laserRayCast.GetPoint(100.0f));
            }
        }
    }

    protected virtual void UpdateLaserState()
    {
        _lr.enabled = LaserActive;
    }
}
