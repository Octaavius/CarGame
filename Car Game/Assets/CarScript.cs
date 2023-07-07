using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarScript : MonoBehaviour
{

    public enum TransmissionTypes
    {
        FW, RW, AW
    }

    public TransmissionTypes transmission;

    public float maxAngel;

    public float motorForce;

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
        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    // Update is called once per frame
    void Update(){
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetKey(KeyCode.Space);
        gearInput = Input.GetKeyDown("e")? 1 : (Input.GetKeyDown("q")? -1 : 0);

        TurnUpdate();
        GearUpdate();
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
        float curAngle = horizontalInput * maxAngel;
        FRWheel.steerAngle = curAngle;
        FLWheel.steerAngle = curAngle;
    }

    void EngineUpdate(){
        float curMotorForce;
        curMotorForce = verticalInput * motorForce;   

        if(gear == 0){
            curMotorForce = -verticalInput * motorForce;
        } 

        if((gear > 0 && curMotorForce >= 0) || (gear == 0 && curMotorForce <= 0)){
            switch(transmission)
            {
                case TransmissionTypes.FW:
                    FRWheel.motorTorque = curMotorForce;
                    FLWheel.motorTorque = curMotorForce;
                    break;

                case TransmissionTypes.RW:
                    RRWheel.motorTorque = curMotorForce;
                    RLWheel.motorTorque = curMotorForce;
                    break;
                
                case TransmissionTypes.AW:
                    FRWheel.motorTorque = curMotorForce;
                    FLWheel.motorTorque = curMotorForce;
                    RRWheel.motorTorque = curMotorForce;
                    RLWheel.motorTorque = curMotorForce;
                    break;
            }  
            BrakeUpdate(false);
        }
        else{
            BrakeUpdate(true);
        }

        wheelUpdate();
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

    void GearUpdate(){
        if((gearInput != 0 && gear > 0 && gear < maxGearNum) || (gear == 0 && gearInput > 0) || (gear == maxGearNum && gearInput < 0)){
            gear += gearInput;
        } 
    }
}
