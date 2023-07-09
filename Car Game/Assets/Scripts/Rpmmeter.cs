using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rpmmeter : MonoBehaviour
{
    private const float MAX_RPM_ANGLE = -140;
    private const float ZERO_RPM_ANGLE = 150;
    
    private Transform needleTransform;
    public GameObject car;
    private Animator animator;

    private float rpmMax;

    private void Awake(){
        needleTransform = transform.Find("needle");
        animator = needleTransform.GetComponent<Animator>();
        rpmMax = 6000f;
    }

    private void Update(){
        needleTransform.eulerAngles = new Vector3(0, 0, GetRpmRotation());
    }

    private float GetRpmRotation(){
        float totalAngleSize = ZERO_RPM_ANGLE - MAX_RPM_ANGLE;

        CarScript carScript = car.GetComponent<CarScript>();

        float rpmNormalized = carScript.getEngineRpm() / rpmMax;
        return ZERO_RPM_ANGLE - rpmNormalized * totalAngleSize;
    }
}
