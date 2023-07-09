using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rpmmeter : MonoBehaviour
{
    private const float MAX_RPM_ANGLE = -140;
    private const float ZERO_RPM_ANGLE = 150;
    
    private Transform needleTransform;
    public GameObject car;

    private float rpmMax;

    private void Awake(){
        needleTransform = transform.Find("needle");

        rpmMax = 6000f;
    }

    private void Update(){
        needleTransform.eulerAngles = new Vector3(0, 0, GetRpmRotation());
    }

    private float GetRpmRotation(){
        float totalAngleSize = ZERO_RPM_ANGLE - MAX_RPM_ANGLE;

        float rpmNormalized = car.GetComponent<CarScript>().getEngineRpm() / rpmMax;
        return ZERO_RPM_ANGLE - rpmNormalized * totalAngleSize;
    }
}
