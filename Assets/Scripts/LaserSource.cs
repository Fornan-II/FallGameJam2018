using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSource : MonoBehaviour
{
    public Transform LaserSourceTransform;
    public bool LaserActive = true;
    public int MaxBounces = 2;
    protected LineRenderer _lr;
    protected int _bounces;

    protected virtual void Start()
    {
        _lr = GetComponent<LineRenderer>();
        UpdateLaserState();
    }

    protected virtual void FixedUpdate()
    {
        UpdateLaserState();

        if(LaserActive)
        {
            _bounces = 0;
            _lr.positionCount = 2;
            _lr.SetPosition(0, LaserSourceTransform.position);
            LaserCast(LaserSourceTransform.position, LaserSourceTransform.forward);
        }
    }

    protected virtual void UpdateLaserState()
    {
        _lr.enabled = LaserActive;
    }

    protected virtual void LaserCast(Vector3 sourcePoint, Vector3 laserDirection)
    {
        Ray laserRayCast = new Ray(sourcePoint, laserDirection);
        RaycastHit hit;
        
        if (Physics.Raycast(laserRayCast, out hit, 100.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            _lr.SetPosition(_lr.positionCount - 1, hit.point);
            LaserReciever reciever = hit.collider.GetComponent<LaserReciever>();
            if (reciever)
            {
                reciever.RecieveLaser();
            }
            if (hit.collider.tag == "Reflective" && _bounces < MaxBounces)
            {
                _bounces++;
                _lr.positionCount++;
                LaserCast(hit.point, Vector3.Reflect(laserDirection, hit.normal));
            }
        }
        else
        {
            _lr.SetPosition(_lr.positionCount - 1, laserRayCast.GetPoint(100.0f));
        }
    }
}
