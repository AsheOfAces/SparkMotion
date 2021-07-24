using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinWheel : MonoBehaviour
{
    [System.Serializable]
    public class WheelSpoke
    {
        [System.Serializable]
        public class FootAlignment
        {
            public AnimationCurve passReach;
            public AnimationCurve leftRight;
        }

        public Vector3 spokeDirection; //direction vector for spoke
        public FootAlignment footAlignment; //target keyframe values

    }


    public int activeSpoke = 0;
    public GameObject playerObject;
    public WheelSpoke[] wheelSpokes;
    float turnCrankSpeed;
    AnimationManager aMan;
    Rigidbody parentBody;
    Vector3 distanceVector;
    float spokeLength;
    public float vDistance, vRatio, normalisedFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        parentBody = playerObject.GetComponent<Rigidbody>();
        aMan = playerObject.GetComponent<AnimationManager>();
        turnCrankSpeed = aMan.turnCrankSpeed;
        distanceVector = new Vector3(0, -1, 0);
        spokeLength = aMan.pinWheelRadius;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation *= Quaternion.AngleAxis(parentBody.velocity.magnitude * turnCrankSpeed * Time.deltaTime, Vector3.right);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[0].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[1].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[2].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[3].spokeDirection) * spokeLength), Color.white);
        Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(wheelSpokes[activeSpoke].spokeDirection) * spokeLength), Color.green);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * spokeLength * -1, Color.cyan);
        vDistance = Vector3.Distance(transform.position + Vector3.up * spokeLength * -1, transform.position + (transform.TransformDirection(wheelSpokes[activeSpoke].spokeDirection) * spokeLength));
        vRatio = Mathf.Clamp01(vDistance/1.4f);
        if(vRatio<0.3)
        {
            vRatio = 0.3f;
            
        }
        normalisedFactor = (vRatio - 0.3f) / 0.7f;

        


        aMan.FootAlignment(1- (normalisedFactor), wheelSpokes[activeSpoke].footAlignment.passReach, wheelSpokes[activeSpoke].footAlignment.leftRight);

    }
}
