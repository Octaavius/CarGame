using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class CarScript : MonoBehaviour
{
    private float[] gearRatioArray = {2.9f, 2.66f, 1.78f, 1.3f, 1f, .74f, .5f}; //first ratio is for reverse gear
    [SerializeField] AnimationCurve engineRpmToTorque;  

    public float enginePower = 4f;

    private float engineRpm = 1000f;
    const float multiplier = 60f * 3.42f / (2f * 3.1415926535897931f) ;

    public float maxAngle = 30;
    protected float angle; 

    public enum TransmissionTypes
    {
        FW, RW, AW
    }

    public TransmissionTypes transmission;

    public float brakeForce = 600f;

    public float HandBrakeForce = 10000f;
    
    [HideInInspector]
    public float verticalInput;
    private float horizontalInput;
    protected ButtonHoldChec brake;
    protected ButtonHoldChec handBrake;

    protected Rigidbody rb;

    public WheelCollider[] wheels = new WheelCollider[4];
    
    public GameObject[] wheelMeshes = new GameObject[4];

    public bool steeringAssistant = true;

    [HideInInspector]
    public int gear = 1;

    [HideInInspector]
    public float curAngle;

    private GameObject backLights = null;
    private GameObject stearingWheel = null;

    private EngineSound es;

    public bool AI = false;

    void Start(){
        // wheels[0] = GameObject.Find("FL").GetComponent<WheelCollider>();
        // wheels[1] = GameObject.Find("FR").GetComponent<WheelCollider>();
        // wheels[2] = GameObject.Find("RL").GetComponent<WheelCollider>();
        // wheels[3] = GameObject.Find("RR").GetComponent<WheelCollider>();
        
        // wheelMeshes[0] = GameObject.Find("FL/wheel");
        // wheelMeshes[1] = GameObject.Find("FR/wheel");
        // wheelMeshes[2] = GameObject.Find("RL/wheel");
        // wheelMeshes[3] = GameObject.Find("RR/wheel");

        brake = GameObject.FindGameObjectWithTag("Brakes").GetComponent<ButtonHoldChec>();
        handBrake = GameObject.FindGameObjectWithTag("handBrakes").GetComponent<ButtonHoldChec>();
        backLights = GameObject.FindGameObjectWithTag("backLights");
        stearingWheel = GameObject.FindGameObjectWithTag("stearingWheel");

        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -.4f, 0f);

        es = GetComponent<EngineSound>();
    }

    // Update is called once per frame
    protected void Update(){
        float verticalModifier = GameObject.FindGameObjectWithTag("GasButton").GetComponent<GasButton>().verticalModifier;
        verticalInput = /*SimpleInput.GetAxis("Vertical") * */verticalModifier;
#if UNITY_EDITOR
        if(SimpleInput.GetAxis("Vertical") < 0)
            verticalInput = SimpleInput.GetAxis("Vertical");
        else verticalInput = Math.Max(SimpleInput.GetAxis("Vertical"), verticalModifier);
#endif
        horizontalInput = SimpleInput.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");

        TurnUpdate();
        EngineUpdate();
        HandBrakeUpdate();  
        if(verticalInput < 0 || brake.buttonPressed){
            BrakeUpdate();
            if(backLights)
                backLights.SetActive(true);
        }
        else {
            for(int i = 0; i < 2; ++i){
                wheels[i].brakeTorque = 0;
            }
            if(backLights)
                backLights.SetActive(false);
        }
        wheelUpdate();
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
        float speed = Vector3.Magnitude(rb.velocity) * 3 * 1.6f;
        
        if(steeringAssistant){
            if (speed < 20) {
                angle = maxAngle;
            } else if (speed < 40){
                angle = 40 - speed/2;
            } else if (speed < 55){
                angle = 60 - speed;
            } else {
                angle = 5;
            }
        } else {
            angle = maxAngle;
        }

        curAngle = horizontalInput * angle;
        if(stearingWheel){
            stearingWheel.transform.localRotation  = horizontalInput != 0 ? Quaternion.Euler(0, -90, 720f / horizontalInput) : Quaternion.Euler(0, -90, 0);
        }
        for(int i = 0; i < 2; ++i){
            wheels[i].steerAngle = curAngle;
        }
    }

    void EngineUpdate(){
        if(Input.GetKeyDown("e") && gear == 0){
            TurnOnDrive();
        } else if (Input.GetKeyDown("q")){
            TurnOnReverse();
        }

        float gearRatio = gearRatioArray[gear];
        
        engineRpm = wheels[0].rpm * gearRatio * multiplier * 0.15f;

        if(engineRpm < 0){
            engineRpm *= -1;
        }
        
        checkGear();
        
        if(engineRpm < 1000f){
            engineRpm = 1000f;
        }

        if(verticalInput < 0) return;

        float curMotorTorque = verticalInput * (engineRpmToTorque.Evaluate(engineRpm) * enginePower / 10f) * gearRatio * 3.42f * 0.7f;
        Debug.Log(curMotorTorque);

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
    }

    void checkGear(){
        //Debug.Log(engineRpm);
        if(gear != 6 && gear != 0 && engineRpm > 4400f){
            //Debug.Log("gearUp");
            //Debug.Log(engineRpm);
            //Debug.Log(engineRpm * (gearRatioArray[gear + 1]) / gearRatioArray[gear]);
            gear++;
            if(es){
                es.topSpeedUp();
            }
        } else if(gear != 1 && gear != 0 && engineRpm * (gearRatioArray[gear - 1])/ gearRatioArray[gear] < 4200f){
            //Debug.Log("gearDown");
            //Debug.Log(engineRpm);
            //Debug.Log(engineRpm * (gearRatioArray[gear - 1]) / gearRatioArray[gear]);
            gear--;

            if(es){
                es.topSpeedDown();
            }
        } else if(engineRpm > 6000f){
            engineRpm = 6000f;
        }
    }

    public void BrakeUpdate(){
        for(int i = 0; i < 4; ++i){
            wheels[i].brakeTorque = brakeForce;
        }
    }

    public void HandBrakeUpdate(){
        bool brakeInput = Input.GetKey(KeyCode.Space) || handBrake.buttonPressed;

        if(brakeInput){
            for(int i = 2; i < 4; ++i){
                wheels[i].brakeTorque = HandBrakeForce;
                WheelFrictionCurve sFriction = wheels[i].sidewaysFriction;
                sFriction.stiffness = 0.5f;
                wheels[i].sidewaysFriction = sFriction;
            }    
        } else {
            for(int i = 2; i < 4; ++i){
                wheels[i].brakeTorque = 0;
                WheelFrictionCurve sFriction = wheels[i].sidewaysFriction;
                sFriction.stiffness = 1f;
                wheels[i].sidewaysFriction = sFriction;
            }
        }
    }

    public float getEngineRpm(){
        return engineRpm;
    } 

    public void TurnOnReverse(){
        gear = 0;
    }

    public void TurnOnDrive(){
        gear = 1;
    }

    public void DriftAssistant(){
        float driftValue = Vector3.Dot(rb.velocity, transform.forward);
        float driftAngle = Mathf.Acos(driftValue) * Mathf.Rad2Deg;
    } 

    private void OnTriggerEnter(Collider col){
        col.gameObject.GetComponent<CollisionChecker>().collider(gameObject);
        if(!AI) col.gameObject.GetComponent<CollisionChecker>().collider(gameObject);
    }

    public void SetInputs(float horizontal, float vertical, int gearInput){
        horizontalInput = horizontal;
        verticalInput = vertical;
        if((gearInput == 0 && gear > 0) || (gearInput == 1 && gear == 0)) gear = gearInput;
    }
}
