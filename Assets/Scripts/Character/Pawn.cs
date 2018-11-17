﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pawn : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpVelocity;
    public Transform cameraPivotTransform;
    public Transform feetCheckTransform;

    public Vector3[] cameraAngles;
    protected int currentCameraAngleIndex = 0;
    protected Coroutine _activeCameraSlerpingCoroutine;

    protected Rigidbody _rb;
    protected Vector3 _movementMapForward;
    protected Vector3 _movementMapRight;

    protected bool _wantsToJump = false;
    protected bool _isGrounded = false;
    protected Vector3 _movementVelocity;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //I'm applying gravity manually to make it feel different.
        _rb.useGravity = false;

        if(cameraAngles == null)
        {
            cameraAngles = new Vector3[]
            {
                cameraPivotTransform.rotation.eulerAngles
            };
        }
        else
        {
            cameraAngles[0] = cameraPivotTransform.rotation.eulerAngles;
        }

        CalculateMovemementMapping();
    }

    protected virtual void FixedUpdate()
    {
        CheckIfGrounded();
        DetermineVelocity();
    }

    #region Input
    public virtual void CalculateMovemementMapping()
    {
        if(cameraPivotTransform)
        {
            _movementMapForward = cameraPivotTransform.up;
            _movementMapForward.y = 0.0f;
            _movementMapForward.Normalize();

            _movementMapRight = cameraPivotTransform.right;
            _movementMapRight.y = 0.0f;
            _movementMapRight.Normalize();
        }
        else
        {
            _movementMapForward = transform.forward;
            _movementMapRight = transform.right;
        }
    }

    public virtual void HandleMovement(Vector2 input)
    {
        Vector3 moveVelocity = _movementMapForward * input.y + _movementMapRight * input.x;

        _movementVelocity = moveVelocity * MovementSpeed;
    }

    public virtual void HandleJump(bool value)
    {
        if (_isGrounded)
        {
            _wantsToJump = value;
        }
        else
        {
            _wantsToJump = false;
        }
    }

    public virtual void HandleInteract(bool value)
    {

    }

    public virtual void HandleSprint(bool value)
    {

    }

    public virtual void HandleSwitchCameraAngle(bool value)
    {
        if (value)
        {
            if (_activeCameraSlerpingCoroutine != null)
            {
                StopCoroutine(_activeCameraSlerpingCoroutine);
            }
            _activeCameraSlerpingCoroutine = StartCoroutine(SlerpToCameraAngle(cameraAngles[currentCameraAngleIndex]));
        }
    }
    #endregion

    protected virtual void CheckIfGrounded()
    {
        float checkDistance = 0.1f;
        if (feetCheckTransform)
        {
            Ray checkRay = new Ray(feetCheckTransform.position, Vector3.down);
            _isGrounded = Physics.Raycast(checkRay, checkDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        }
        else
        {
            Debug.LogWarning("No feetCheckTransform object assigned to " + name + "!");
            _isGrounded = false;
        }
    }

    protected virtual void DetermineVelocity()
    {
        float yVel = _rb.velocity.y;
        Vector3 newVelocity = _movementVelocity;

        if(_wantsToJump)
        {
            yVel += JumpVelocity;
            _wantsToJump = false;
        }
        else
        {
            yVel -= 20.0f * Time.fixedDeltaTime;
        }
        newVelocity.y = yVel;

        _rb.velocity = newVelocity;
    }

    protected IEnumerator SlerpToCameraAngle(Vector3 targetAngle)
    {
        float timeToReachTargetAngle = 1.0f;
        Vector3 startingAngle = cameraPivotTransform.rotation.eulerAngles;

        Debug.DrawRay(cameraPivotTransform.position, startingAngle, Color.blue);
        Debug.DrawRay(cameraPivotTransform.position, targetAngle, Color.red);
        UnityEditor.EditorApplication.isPaused = true;

        for (float timer = 0.0f; timer < timeToReachTargetAngle; timer += Time.deltaTime)
        {
            yield return null;
            cameraPivotTransform.rotation = Quaternion.Euler(Vector3.Slerp(startingAngle, targetAngle, timer / timeToReachTargetAngle));
        }

        cameraPivotTransform.rotation = Quaternion.Euler(targetAngle);
        currentCameraAngleIndex++;
        if(currentCameraAngleIndex >= cameraAngles.Length)
        {
            currentCameraAngleIndex = 0;
        }

        _activeCameraSlerpingCoroutine = null;
    }
}
