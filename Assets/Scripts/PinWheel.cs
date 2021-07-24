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
    
    // Start is called before the first frame update
    void Start()
    {
        parentBody = playerObject.GetComponent<Rigidbody>();
        aMan = playerObject.GetComponent<AnimationManager>();
        turnCrankSpeed = aMan.turnCrankSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation *= Quaternion.AngleAxis(parentBody.velocity.magnitude * turnCrankSpeed * Time.deltaTime, Vector3.right);

        //todo: use an actual wheel
        RaycastHit fwHit, upHit, bkHit, dwHit;
        //pinwheel goes here
        if (Physics.Raycast(transform.position, transform.forward, out fwHit, 50, 0))
        {
            Debug.DrawRay(transform.position, transform.forward * fwHit.distance, Color.yellow);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 50, Color.white);

        }
    }
}
