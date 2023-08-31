using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreeCamera : MonoBehaviour
{

    public Transform Target;

    //public GameObject fixedCamera;
    //public GameObject freeCamera;

    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private bool isFixed = true;

    private Rigidbody rigidBody;

    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        rigidBody = GetComponent<Rigidbody>();
        
        //freeCamera.SetActive(false);
        //fixedCamera.SetActive(true);

        if(rigidBody != null){
            rigidBody.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void LateUpdate(){
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        if(Input.GetKeyDown("r")){
            changeCamera();
        }

        if(Target && !isFixed){
            x += deltaX * xSpeed * distance * 0.02f;
            y -= deltaY * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            //freeCamera.transform.rotation = rotation;
            transform.rotation = rotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + Target.position;

            //freeCamera.transform.position = position;
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

    public void changeCamera(){
        // if(isFixed){
        //     fixedCamera.SetActive(false);
        //     freeCamera.SetActive(true);
        // }
        // else{
        //     freeCamera.SetActive(false);
        //     fixedCamera.SetActive(true);
        // }
        //isFixed = isFixed ? false : true;
        isFixed = !isFixed;
    }
}
