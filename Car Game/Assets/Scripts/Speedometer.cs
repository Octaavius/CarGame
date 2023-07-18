using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    private const float MAX_SPEED_ANGLE = -140;
    private const float ZERO_SPEED_ANGLE = 150;
    
    private Transform needle;
    private Transform speed;
    
    public GameObject car;
    

    private float speedMax;

    private void Awake(){
        needle = transform.Find("needle");
        speed = transform.Find("speed");

        speedMax = 200f;
    }

    private void Update(){
        needle.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
        speed.GetComponent<Text>().text = ((int)(Vector3.Magnitude(car.GetComponent<Rigidbody>().velocity)*3)).ToString(); 
    }

    private float GetSpeedRotation(){
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speed = Vector3.Magnitude(car.GetComponent<Rigidbody>().velocity)*3;


        float speedNormalized = speed / speedMax;
        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }
}
