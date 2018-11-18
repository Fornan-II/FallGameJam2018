using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yKill : MonoBehaviour
{
    public float killYLevel;

    protected Vector3 _startingPos;

    protected virtual void Start()
    {
        _startingPos = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        if(transform.position.y < killYLevel)
        {
            Respawn();
        }
    }

    public virtual void Respawn()
    {
        transform.position = _startingPos;

        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
