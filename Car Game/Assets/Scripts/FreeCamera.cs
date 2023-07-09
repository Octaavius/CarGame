using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreeCamera : MonoBehaviour
{

    public Transform target;

    public GameObject cameraCar;

    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private bool fix = true;

    private Rigidbody rigidBody;

    float x = 0.0f;
    float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        rigidBody = GetComponent<Rigidbody>();
        
        GetComponent<Camera>().enabled = false;
        cameraCar.SetActive(true);

        if(rigidBody != null){
            rigidBody.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void LateUpdate(){
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        if(Input.GetKeyDown("r")){
            fix = fix ? false : true;
            if(!fix){
                cameraCar.SetActive(false);
                GetComponent<Camera>().enabled = true;
            }
            else{
                GetComponent<Camera>().enabled = false;
                cameraCar.SetActive(true);
            }
        }
        
        if(target && !fix){
            x += deltaX * xSpeed * distance * 0.02f;
            y-= deltaY * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.position = position;
        }


    }

    public static float ClampAngle(float angle, float min, float max){
        if(angle < -360F) {
            angle += 360F;
        }
        if (angle > 360F){
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    } 
}
