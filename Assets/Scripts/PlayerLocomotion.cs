using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Rigidbody playerRigidbody;

    Vector3 moveDirection;
    Transform cameraObject;
    public Transform playerCenter;
    public Transform referenceCameraTransform;




    public float movementSpeed = 7;
    public float rotationSpeed = 15;
    public LayerMask groundLayer;
    public float inAirTimer;
    public float fallingVelocity;
    public float leapingVelocity;
    public bool isGrounded;
    public bool airTimeFlag;

    private Vector3 prevPos;
    public Vector3 actualVelocity;
    private float referenceY;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        StartCoroutine(CalcVelocity());
    }

    public void UltimateMovementHandler()
    {
        HandleFalling();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward.normalized * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right.normalized * inputManager.horizontalInput;
        //moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }    

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFalling()
    {
        Vector3 raycastOrigin = transform.position;
        if(!isGrounded)
        {
            //TODO: Play fall animation here
            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up*fallingVelocity*inAirTimer);

            
        }
        if (Physics.CheckSphere(transform.position+new Vector3(0,0.1f,0), 0.2f, groundLayer))
        {
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
       
    }

    IEnumerator CalcVelocity()
    {

        while (Application.isPlaying)
        {
            // Position at frame start
            prevPos = transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            // Calculate velocity: Velocity = DeltaPosition / DeltaTime
            actualVelocity = (prevPos - transform.position) / Time.deltaTime;
            Debug.Log(playerRigidbody.velocity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

}
