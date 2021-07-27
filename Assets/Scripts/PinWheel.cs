using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class PinWheel : MonoBehaviour
{
    [System.Serializable]
    public class WheelSpoke
    {
        public Vector3 spokeDirection; 
        public AnimationClip keyState;
    }
    public int activeSpoke = 0;
    public GameObject playerObject;
    public WheelSpoke[] wheelSpokes;
    public float turnCrankSpeed;
    
    public float spokeLength;
    public float vDistance, vRatio, normalisedFactor;

    AnimationManager aMan;
    Rigidbody parentBody;
    Vector3 distanceVector;

    Quaternion targetRotation;
    bool surveyorPassed = false;

    void Start()
    {
        parentBody = playerObject.GetComponent<Rigidbody>();
        aMan = playerObject.GetComponent<AnimationManager>();
        distanceVector = new Vector3(0, -1, 0);
        targetRotation = Quaternion.identity;
    }
    void Update()
    {
        targetRotation = targetRotation * Quaternion.AngleAxis(parentBody.velocity.magnitude * turnCrankSpeed * Time.deltaTime, Vector3.right);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, parentBody.velocity.magnitude * turnCrankSpeed * Time.deltaTime);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[0].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[1].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[2].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[3].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[activeSpoke].spokeDirection) * spokeLength), Color.green);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * spokeLength * -1, Color.cyan);
        vDistance = Vector3.Distance(transform.position + Vector3.up * spokeLength * -1, transform.position + (transform.TransformDirection(wheelSpokes[activeSpoke].spokeDirection) * spokeLength));
        vRatio = Mathf.Clamp01(vDistance/1.4f);
        if(vDistance <= 0.3)
        {
            surveyorPassed = true;
        }
        normalisedFactor = Mathf.Clamp01((vRatio - 0.3f) / 0.7f);

        if (normalisedFactor == 0)
        {
            if(surveyorPassed)
            {
                if (activeSpoke == 3)
                {
                    activeSpoke = 0;
                }
                else activeSpoke++;
                if(parentBody.velocity.magnitude < 6 && parentBody.velocity.magnitude > 0)
                {
                    aMan.PlayState(1, true, true, activeSpoke);
                }
                if (parentBody.velocity.magnitude >= 6)
                {
                    aMan.PlayState(2, true, true, activeSpoke);
                }
                surveyorPassed = false;
            }
        }
    }
}
