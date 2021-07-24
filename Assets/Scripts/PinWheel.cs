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
    }
}
