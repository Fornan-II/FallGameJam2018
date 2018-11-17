using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pawn : MonoBehaviour
{
    public float MovementSpeed;
    public float SprintMultiplier = 1.5f;
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

    protected bool _isSprinting = false;

    protected Interactable interactableObject;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //I'm applying gravity manually to make it feel different.
        _rb.useGravity = false;

        CalculateMovementMapping();
    }

    protected virtual void FixedUpdate()
    {
        CheckIfGrounded();
        DetermineVelocity();
        FindInteractAbleObject();
    }

    #region Input
    public virtual void CalculateMovementMapping()
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

        if(_isSprinting)
        {
            _movementVelocity *= SprintMultiplier;
        }
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
        if(value && interactableObject)
        {
            interactableObject.Interact(gameObject);
        }
    }

    public virtual void HandleSprint(bool value)
    {
        if(value)
        {
            _isSprinting = !_isSprinting;
        }
    }

    public virtual void HandleSwitchCameraAngle(bool value)
    {
        if (value)
        {
            if (_activeCameraSlerpingCoroutine == null)
            {
                _activeCameraSlerpingCoroutine = StartCoroutine(SlerpToNextCameraAngle());
            }
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

    protected IEnumerator SlerpToNextCameraAngle()
    {
        currentCameraAngleIndex++;
        if (currentCameraAngleIndex >= cameraAngles.Length)
        {
            currentCameraAngleIndex = 0;
        }
        
        float timeToReachTargetAngle = 0.7f;
        Quaternion startingRotation = cameraPivotTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(cameraAngles[currentCameraAngleIndex]);

        for (float timer = 0.0f; timer < timeToReachTargetAngle; timer += Time.deltaTime)
        {
            yield return null;
            cameraPivotTransform.rotation = Quaternion.Slerp(startingRotation, targetRotation, timer / timeToReachTargetAngle);
            CalculateMovementMapping();
        }

        cameraPivotTransform.rotation = targetRotation;
        _activeCameraSlerpingCoroutine = null;
    }

    protected virtual void FindInteractAbleObject()
    {
        float checkRadius = 1f;
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, checkRadius);
        List<Interactable> nearbyInteractables = new List<Interactable>();
        foreach(Collider c in nearbyColliders)
        {
            Interactable i = c.GetComponent<Interactable>();
            if(i)
            {
                nearbyInteractables.Add(i);
            }
        }

        if(nearbyInteractables.Count > 0)
        {
            interactableObject = nearbyInteractables[0];
            float interactableDistance = Vector3.Distance(transform.position, interactableObject.transform.position);
            for(int i = 1; i < nearbyInteractables.Count; i++)
            {
                if (Vector3.Distance(transform.position, nearbyInteractables[i].transform.position) < interactableDistance)
                {
                    interactableObject = nearbyInteractables[i];
                    interactableDistance = Vector3.Distance(transform.position, interactableObject.transform.position);
                }
            }
        }
        else
        {
            interactableObject = null;
        }
    }
}
