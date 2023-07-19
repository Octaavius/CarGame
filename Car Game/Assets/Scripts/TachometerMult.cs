using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TachometerMult : MonoBehaviour
{
    private const float MAX_RPM_ANGLE = -45;
    private const float ZERO_RPM_ANGLE = 150;
    
    private Transform needle;
    public Transform gearNumber;

    public MultCarScript carScript;

    private float rpmMax;

    private void Awake(){
        needle = transform.Find("needle");
        gearNumber = transform.Find("gear");
        rpmMax = 6000f;
    }

    private void Update(){
        needle.eulerAngles = new Vector3(0, 0, GetRpmRotation());
        gearNumber.GetComponent<Text>().text = carScript.gear.ToString(); 
    }


    private float GetRpmRotation(){
        float totalAngleSize = ZERO_RPM_ANGLE - MAX_RPM_ANGLE;
        float rpmNormalized = carScript.getEngineRpm() / rpmMax;
        return ZERO_RPM_ANGLE - rpmNormalized * totalAngleSize;
    }
}
