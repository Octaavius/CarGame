﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour{
    private Rigidbody rb;

    [Header("Suspention")]
    //length without spring 
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springForce;
    private float springVelocity;
    private float damperForce;

    private Vector3 suspensionForce;

    [Header("Wheel")]
    public float wheelRadius;

    
    void Start(){
        rb = transform.root.GetChild(0).GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void FixedUpdate(){
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius)){
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;

            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;

            rb.AddForceAtPosition(suspensionForce, hit.point);
        }
    }
}
