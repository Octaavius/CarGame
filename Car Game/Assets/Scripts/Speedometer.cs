using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private const float MAX_SPEED_ANGLE = -140;
    private const float ZERO_SPEED_ANGLE = 150;
    
    private Transform needleTransform;
    public GameObject car;

    private float speedMax;

    private void Awake(){
        needleTransform = transform.Find("needle");

        speedMax = 200f;
    }

    private void Update(){
        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private float GetSpeedRotation(){
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speed = Vector3.Magnitude(car.GetComponent<Rigidbody>().velocity)*3;


        float speedNormalized = speed / speedMax;
        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }
}
