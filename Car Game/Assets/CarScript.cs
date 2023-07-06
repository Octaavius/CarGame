using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float maxAngle;

    public float motorForce;

    public float brakeForce;
    public float siski_skin_pls;
    
    private bool brakeInput;
    private float verticalInput;
    private float horizontalInput;

    public WheelCollider FLWheel;
    public WheelCollider FRWheel;
    public WheelCollider RLWheel;
    public WheelCollider RRWheel;

    public GameObject FLWheelMesh;
    public GameObject FRWheelMesh;
    public GameObject RLWheelMesh;
    public GameObject RRWheelMesh;
    

    void Start(){
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    // Update is called once per frame
    void Update(){
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetKey(KeyCode.Space);
        wheelUpdate();
        TurnUpdate();
        EngineUpdate();
        if(brakeInput){
            BrakeUpdate();
        }
    }

    void wheelUpdate(){
        singleWheelUpdate(FLWheel, FLWheelMesh);
        
        singleWheelUpdate(FRWheel, FRWheelMesh);
        
        singleWheelUpdate(RLWheel, RLWheelMesh);
    
        singleWheelUpdate(RRWheel, RRWheelMesh);
    }

    void singleWheelUpdate(WheelCollider collider, GameObject wheel){
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheel.GetComponent<Transform>().rotation = rot;
        wheel.GetComponent<Transform>().position = pos; 
    }

    void TurnUpdate(){
        float curAngle = horizontalInput * maxAngle;
        FRWheel.steerAngle = curAngle;
        FLWheel.steerAngle = curAngle;
    }

    void EngineUpdate(){
        float curMotorForce = verticalInput * motorForce;
        FRWheel.motorTorque = curMotorForce;
        FLWheel.motorTorque = curMotorForce;
    }

    void BrakeUpdate(){
        FRWheel.brakeTorque = brakeForce;
        FLWheel.brakeTorque = brakeForce;
        RLWheel.brakeTorque = brakeForce;
        RRWheel.brakeTorque = brakeForce;
    }
}
