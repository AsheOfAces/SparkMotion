using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject animPinWheel;
    public float pinWheelRadius = 0.75f, turnCrankSpeed = 50;
    public LayerMask lMask;
    int locoState, activeFoot, passReach, velocity;

    float moveVelocity;
    Rigidbody rb;

    private void Awake()
    {
        
        locoState = Animator.StringToHash("Walk-Run");
        activeFoot = Animator.StringToHash("Left-Right");
        passReach = Animator.StringToHash("Pass-Reach");
        velocity = Animator.StringToHash("Velocity");
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        RaycastHit fwHit, upHit, bkHit, dwHit;
        //pinwheel goes here
        if (Physics.Raycast(animPinWheel.transform.position, animPinWheel.transform.forward, out fwHit, pinWheelRadius, lMask))
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.forward * fwHit.distance, Color.yellow);
            animator.SetFloat(activeFoot, 0);
            animator.SetFloat(passReach, 0);

        }
        else
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.forward * pinWheelRadius, Color.white);
            
        }
        if (Physics.Raycast(animPinWheel.transform.position, animPinWheel.transform.up, out upHit, pinWheelRadius, lMask))
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.up * upHit.distance, Color.yellow);
            animator.SetFloat(activeFoot, 0);
            animator.SetFloat(passReach, 1);
        }
        else
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.up * pinWheelRadius, Color.white);
            
        }
        if (Physics.Raycast(animPinWheel.transform.position, animPinWheel.transform.forward * (-1f), out bkHit, pinWheelRadius, lMask))
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.forward * (-1f) * bkHit.distance, Color.yellow);
            animator.SetFloat(activeFoot, 1);
            animator.SetFloat(passReach, 0);
        }
        else
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.forward* (-1f) * pinWheelRadius, Color.white);
            
        }
        if (Physics.Raycast(animPinWheel.transform.position, animPinWheel.transform.up * (-1f), out dwHit, pinWheelRadius, lMask))
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.up* (-1f) * dwHit.distance, Color.yellow);
            animator.SetFloat(activeFoot, 1);
            animator.SetFloat(passReach, 1);
        }
        else
        {
            Debug.DrawRay(animPinWheel.transform.position, animPinWheel.transform.up * (-1f) * pinWheelRadius, Color.white);
            
        }

        moveVelocity = rb.velocity.magnitude;
        animator.SetFloat(velocity, moveVelocity);
    }

    public void UpdateAnimatorValues(float hMov, float vMov)
    {

    }
}
