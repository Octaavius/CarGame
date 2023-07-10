using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarScript : MonoBehaviour
{
    private float[] gearRatioArray = {2.9f, 2.66f, 1.78f, 1.3f, 1f, .74f, .5f}; //first ratio is for reverse gear
    [SerializeField] AnimationCurve engineRpmToTorque;

    private float enginePower = 1000f;    

    private float maxEngineRpm = 1000f;
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
    
    private static float verticalInput;
    private static float horizontalInput;

    private Rigidbody rb;

    public WheelCollider[] wheels = new WheelCollider[4];
    
    public GameObject[] wheelMeshes = new GameObject[4];

    public static int gear = 1;

    void Start(){
        wheels[0] = GameObject.Find("FL").GetComponent<WheelCollider>();
        wheels[1] = GameObject.Find("FR").GetComponent<WheelCollider>();
        wheels[2] = GameObject.Find("RL").GetComponent<WheelCollider>();
        wheels[3] = GameObject.Find("RR").GetComponent<WheelCollider>();
        
        wheelMeshes[0] = GameObject.Find("FL/wheel");
        wheelMeshes[1] = GameObject.Find("FR/wheel");
        wheelMeshes[2] = GameObject.Find("RL/wheel");
        wheelMeshes[3] = GameObject.Find("RR/wheel");

        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    // Update is called once per frame
    void Update(){
        verticalInput = SimpleInput.GetAxis("Vertical");
        horizontalInput = SimpleInput.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");
        
        TurnUpdate();
        if(verticalInput < 0){
            BrakeUpdate();
        }
        else {
            for(int i = 0; i < 2; ++i){
                wheels[i].brakeTorque = 0;
            }
        }
        EngineUpdate();
        wheelUpdate();
        HandBrakeUpdate();  
    }

    void wheelUpdate(){
        for(int i = 0; i < 4; ++i){
            singleWheelUpdate(wheels[i], wheelMeshes[i]);
        }
    }

    void singleWheelUpdate(WheelCollider collider, GameObject wheel){
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheel.GetComponent<Transform>().rotation = rot;
        wheel.GetComponent<Transform>().position = pos; 
    }

    void TurnUpdate(){
        float speed = Vector3.Magnitude(rb.velocity)*3;
        
        if(speed > 50) maxAngle = 10;
        else maxAngle = 15;

        float curAngle = horizontalInput * maxAngle;
        for(int i = 0; i < 2; ++i){
            wheels[i].steerAngle = curAngle;
        }
    }

    void EngineUpdate(){
        if(Input.GetKeyDown("e") && gear == 0){
            TurnDrive();
        } else if (Input.GetKeyDown("q")){
            TurnReverse();
        }

        float gearRatio = gearRatioArray[gear];
        
        maxEngineRpm = wheels[0].rpm * gearRatio * multiplier * 0.342f * 0.7f;
        
        if(verticalInput >= 0) engineRpm = maxEngineRpm * verticalInput;

        float curMotorTorque = 0;
        
        if(engineRpm < 1000f){
            engineRpm = 1000f;
        }
        curMotorTorque = engineRpmToTorque.Evaluate(engineRpm) * gearRatio * 0.342f * 0.7f;
        
        if(gear == 1 || gear == 0) {   
            curMotorTorque *= verticalInput;
        }

        if(verticalInput < 0) return;

        if(gear == 0){
            curMotorTorque *= -1;
        } 

        switch(transmission)
        {
            case TransmissionTypes.FW:
                for(int i = 0; i < 2; ++i){
                    wheels[i].motorTorque = curMotorTorque / 2;
                }
                break;

            case TransmissionTypes.RW:
                for(int i = 2; i < 4; ++i){
                    wheels[i].motorTorque = curMotorTorque / 2;
                }
                break;
            
            case TransmissionTypes.AW:
                for(int i = 0; i < 4; ++i){
                    wheels[i].motorTorque = curMotorTorque / 4;
                }
                break;
        }  
        checkGear();
        //check if we need to shift a gear

        
    }

    void checkGear(){
        if(engineRpm > 5000f && gear != 6){
            gear++;
        } else if(engineRpm < 2000f && gear != 1 && gear != 0){
            gear--;
        } else if(engineRpm > 6000f && gear == 6){
            engineRpm = 6000f;
        }
    }

    void BrakeUpdate(){
        for(int i = 0; i < 4; ++i){
            wheels[i].brakeTorque = brakeForce;
        }
    }

    void HandBrakeUpdate(){
        bool brakeInput = Input.GetKey(KeyCode.Space);

        if(brakeInput){
            for(int i = 2; i < 4; ++i){
                wheels[i].brakeTorque = HandBrakeForce;
            }    
        } else {
            for(int i = 2; i < 4; ++i){
                wheels[i].brakeTorque = 0;
            }
        }
    }

    public float getEngineRpm(){
        return engineRpm;
    } 

    public void TurnReverse(){
        gear = 0;
    }

    public void TurnDrive(){
        gear = 1;
    }
}
