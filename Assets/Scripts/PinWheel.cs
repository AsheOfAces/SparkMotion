using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinWheel : MonoBehaviour
{
    public GameObject playerObject;
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
        transform.localRotation *= Quaternion.AngleAxis(parentBody.velocity.magnitude * turnCrankSpeed * Time.deltaTime, Vector3.left);

    }
}
