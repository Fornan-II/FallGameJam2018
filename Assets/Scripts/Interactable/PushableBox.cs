using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PushableBox : Interactable
{
    public float PushDistance = 1.0f;
    protected Rigidbody _rb;
    protected Collider _col;
    protected bool _isBeingPushed = false;
    public bool CanBePushed { get { return !_isBeingPushed; } }

    protected Vector3 _startPosition;
    protected Vector3 _targetPosition;


    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    protected virtual void FixedUpdate()
    {
        if(_isBeingPushed)
        {
            HandlePushing();
        }
    }

    public override void Interact(GameObject interacter)
    {
        if(!AllowInteract) { return; }

        Debug.Log(interacter.name + " interacts!");

        Vector3 deltaPosition = interacter.transform.position - transform.position;
        deltaPosition.y = 0.0f;
        if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.z))
        {
            deltaPosition.z = 0.0f;
        }
        else
        {
            deltaPosition.x = 0.0f;
        }

        deltaPosition.Normalize();

        PushBox(deltaPosition * -1f);
    }

    public bool PushBox(Vector3 direction)
    {
        if(_isBeingPushed) { return false; }
        
        if (Physics.BoxCast(_col.bounds.center, _col.bounds.size / 2.1f, direction, transform.rotation, PushDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Failed sweep");
            return false;
        }
        else
        {
            float checkDistance = 0.1f;
            Vector3 pushPosition = transform.position;
            pushPosition.y = _col.bounds.min.y;
            pushPosition += (direction * PushDistance);
            Ray groundCheckRay = new Ray(pushPosition + (Vector3.up * checkDistance * 0.5f), Vector3.down);
            Debug.DrawRay(groundCheckRay.origin, groundCheckRay.direction * checkDistance, Color.green, 1.0f);
            if(Physics.Raycast(groundCheckRay, checkDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                _isBeingPushed = true;
                _startPosition = transform.position;
                _targetPosition = transform.position + (direction * PushDistance);
                Debug.Log("Success");
                return true;
            }
            else
            {
                Debug.Log("Failed ground check");
                return false;
            }
        }
    }

    protected virtual void HandlePushing()
    {
        //float distTravelled = Vector3.Distance(transform.position, _startPosition) / PushDistance;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.2f);
        if(Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            transform.position = _targetPosition;
            _isBeingPushed = false;
        }
    }
}
