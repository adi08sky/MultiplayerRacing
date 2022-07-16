using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportDriveScript : MonoBehaviour
{
    Rigidbody rb;
    float lastTimeChecked;
    public float antiRoll = 5000.0f;

    [Header("0 - lewe ko³o, 1 - prawe ko³o")]
    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] backWheels = new WheelCollider[2];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || rb.velocity.magnitude > 1)
        {
            lastTimeChecked = Time.time;
        }
        if (Time.time > lastTimeChecked + 3)
        {
            TurnBackCar();
        }
    }

    void FixedUpdate()
    {
        HoldWheelOnGround(frontWheels);
        HoldWheelOnGround(backWheels);
    }

    void TurnBackCar()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
    void HoldWheelOnGround(WheelCollider[] wheels)
    {
        WheelHit hit;
        float leftRiding = 1.0f;
        float rightRiding = 1.0f;

        bool groundedL = wheels[0].GetGroundHit(out hit);
        if (groundedL) leftRiding = (-wheels[0].transform.InverseTransformPoint(hit.point).y - wheels[0].radius) / wheels[0].suspensionDistance;

        bool groundedR = wheels[1].GetGroundHit(out hit);
        if (groundedR) rightRiding = (-wheels[1].transform.InverseTransformPoint(hit.point).y - wheels[1].radius) / wheels[1].suspensionDistance;

        float antiRollForce = (leftRiding - rightRiding) * antiRoll;

        if (groundedL) rb.AddForceAtPosition(wheels[0].transform.up * -antiRollForce, wheels[0].transform.position);
        if (groundedR) rb.AddForceAtPosition(wheels[1].transform.up * antiRollForce, wheels[1].transform.position);
    }
}
