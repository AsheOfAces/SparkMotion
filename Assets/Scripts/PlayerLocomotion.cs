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
    Transform t_Reference;
    Transform t_MeshReference;
    public Transform playerCenter;
    public Transform referenceCameraTransform;
    public Transform pupModel;




    public float movementSpeed = 7;
    public float rotationSpeed = 15;
    public LayerMask groundLayer;
    public float inAirTimer;
    public float fallingVelocity;
    public float leapingVelocity;
    public float tilt;
    public bool isGrounded;
    public bool airTimeFlag;

    private Vector3 prevPos;
    public Vector3 actualVelocity;
    private float referenceY;
    private Quaternion targetRotation;
    private void Awake()
    {
        targetRotation = Quaternion.identity;
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        StartCoroutine(CalcVelocity());
        t_Reference = new GameObject().transform;
        t_MeshReference = new GameObject().transform;
    }

    public void UltimateMovementHandler()
    {
        t_Reference.eulerAngles = new Vector3(0, cameraObject.eulerAngles.y, 0);
        t_MeshReference.eulerAngles = new Vector3(0, pupModel.eulerAngles.y, 0);
        HandleFalling();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        


        moveDirection = t_Reference.forward.normalized * inputManager.verticalInput;
        moveDirection = moveDirection + t_Reference.right.normalized * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;


        if (isGrounded && moveDirection.magnitude == 0)
        {
            inputManager.animationManager.PlayState(0, false, false, 0);
        }
    }

    private void HandleRotation()
    {
        //old roatator code
        Vector3 targetDirection = Vector3.zero;
        Vector3 vDirection = playerRigidbody.velocity.normalized;
        targetDirection = vDirection;
        
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = t_MeshReference.transform.forward;
            targetRotation = Quaternion.LookRotation(targetDirection);
        }
        else
        {
            targetDirection -= new Vector3(0,tilt,0);
            targetRotation = Quaternion.LookRotation(targetDirection);
        }    

        
        Quaternion playerRotation = Quaternion.Slerp(pupModel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        pupModel.transform.rotation = playerRotation;

        

        //Only rotate the mesh.
        
    }

    private void HandleFalling()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        if(!isGrounded)
        {
            //TODO: Play fall animation here
            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up*fallingVelocity*inAirTimer);

            
        }
        if(Physics.SphereCast(raycastOrigin + new Vector3(0,0.5f,0),0.2f, -transform.up,out hit, 0.51f,groundLayer))
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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

}
