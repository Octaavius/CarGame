using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarScript : MonoBehaviour
{
    private float[] gearRatioArray = {2.9f, 2.66f, 1.78f, 1.3f, 1f, .74f, .5f}; //first ratio is for reverse gear
    [SerializeField] AnimationCurve enginePower;

    private float engineRpm = 1000f; 
    const float multiplier = 60f / (2f * 3.1415926535897931f);

    public float maxAngle;

    public enum TransmissionTypes
    {
        FW, RW, AW
    }


    public TransmissionTypes transmission;

    public float brakeForce;

    public float HandBrakeForce;
    public float siski_skin_pls;

    public int maxGearNum = 6;
    
    private bool brakeInput;
    private float verticalInput;
    private float horizontalInput;
    private int gearInput;

    private Rigidbody rb;

    public WheelCollider FLWheel;
    public WheelCollider FRWheel;
    public WheelCollider RLWheel;
    public WheelCollider RRWheel;

    public GameObject FLWheelMesh;
    public GameObject FRWheelMesh;
    public GameObject RLWheelMesh;
    public GameObject RRWheelMesh;

    public int gear = 1;

    void Start(){
        //WheelCollider FLWheel1 = GameObject.Find("FL").GetComponent<WheelCollider>();
        // FRWheel = GameObject.Find("FR").GetComponent<WheelCollider>();
        // RLWheel = GameObject.Find("RL").GetComponent<WheelCollider>();
        // RRWheel = GameObject.Find("RR").GetComponent<WheelCollider>();

        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    // Update is called once per frame
    void Update(){
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetKey(KeyCode.Space);
        

        TurnUpdate();
        BrakeUpdate(verticalInput < 0);
        EngineUpdate();
        HandBrakeUpdate();  
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
        float curMotorTorque;
        float gearRatio = gearRatioArray[gear];
        if(Input.GetKeyDown("e") && gear == 0){
            gear = 1;
        } else if (Input.GetKeyDown("q")){
            gear = 0;
        }

        engineRpm = FLWheel.rpm * gearRatio * multiplier * 0.342f * 0.7f;
        if(engineRpm < 1000f){
            engineRpm = 1000f;
        }

        curMotorTorque = verticalInput * enginePower.Evaluate(engineRpm) * gearRatio / 5;
        
        if(gear == 0){
            curMotorTorque *= -1;
        } 

        switch(transmission)
        {
            case TransmissionTypes.FW:
                FRWheel.motorTorque = curMotorTorque / 2;
                FLWheel.motorTorque = curMotorTorque / 2;
                break;

            case TransmissionTypes.RW:
                RRWheel.motorTorque = curMotorTorque / 2;
                RLWheel.motorTorque = curMotorTorque / 2;
                break;
            
            case TransmissionTypes.AW:
                FRWheel.motorTorque = curMotorTorque / 4;
                FLWheel.motorTorque = curMotorTorque / 4;
                RRWheel.motorTorque = curMotorTorque / 4;
                RLWheel.motorTorque = curMotorTorque / 4;
                break;
        }  
        // Debug.Log(engineRpm);
        // Debug.Log(gear);
        checkGear();
        //check if we need to shift a gear

        wheelUpdate();
    }

    void checkGear(){
        if(engineRpm > 5000f && gear != 6){
            gear++;
        } else if(engineRpm < 3000f && gear != 1 && gear != 0){
            gear--;
        }
    }

    void BrakeUpdate(bool stop){
        if(stop){
            RLWheel.brakeTorque = brakeForce;
            RRWheel.brakeTorque = brakeForce;
            FLWheel.brakeTorque = brakeForce;
            FRWheel.brakeTorque = brakeForce;
        }
        else{
            RLWheel.brakeTorque = 0;
            RRWheel.brakeTorque = 0;
            FLWheel.brakeTorque = 0;
            FRWheel.brakeTorque = 0;
        }
        
    }

    void HandBrakeUpdate(){
        if(brakeInput){
            RLWheel.brakeTorque = HandBrakeForce;
            RRWheel.brakeTorque = HandBrakeForce;
        }
    }


}
