using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject animPinWheel;
    public float pinWheelRadius = 0.75f, turnCrankSpeed = 50;
    public LayerMask lMask;
    public AnimationCurve[] animationCurves;


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

    public void FootAlignment(float alpha, AnimationCurve prCurve, AnimationCurve lfCurve)
    {
        animator.SetFloat(passReach, prCurve.Evaluate(alpha));
        animator.SetFloat(activeFoot, lfCurve.Evaluate(alpha));
    }

    public void UpdateAnimatorValues(float hMov, float vMov)
    {

    }
}
