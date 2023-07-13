using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCameraRotate : MonoBehaviour
{
    // References
    [SerializeField] private Transform cameraTransform;

    // Player settings
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;

    // Touch detection
    private float minHeght;

    // Camera control
    private Vector2 lookInput;

    // Player movement
    private Vector2 moveTouchStartPosition;

    public Transform Target;

    float x = 0;
    float y = 0;

    public float distance = 2.5f;

    private static bool freeMode = false;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        // only calculate once
        minHeght = Screen.height / 2;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Handles input
        GetTouchInput();
    }

    void GetTouchInput() {
        if(Input.touchCount == 0){
            lookInput = Vector2.zero;
        }
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if(t.position.y > minHeght)
            {
                // Check each touch's phase
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;

                        break;
                    
                    case TouchPhase.Moved:
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                        break;

                    case TouchPhase.Canceled:
                        lookInput = Vector2.zero;
                        break;

                    case TouchPhase.Ended:
                        lookInput = Vector2.zero;
                        break;
                }
            }
            // else{
            //     lookInput = Vector2.zero;
            // }
        }
    }

    void LateUpdate() {
        float deltaX = lookInput.x;
        float deltaY = lookInput.y;
    
        x += deltaX * moveSpeed  * 0.02f;
        y-= deltaY * moveSpeed * 0.02f;

        y = ClampAngle(y, 10f, 90f);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + Target.position;

        transform.position = position;
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

    public void FreeModeOff(){
        //GameObject parent = GameObject.FindGameObjectWithTag("Car");
        transform.parent = null;
        freeMode = false;
        GetComponent<Follow>().enabled = true;
        this.enabled = false;
    }

    public void FreeModeOn(){
        GameObject parent = GameObject.FindGameObjectWithTag("Car");
        transform.parent = parent.transform;
        freeMode = true;
        this.enabled = true;
        GetComponent<Follow>().enabled = false;
    }

    public void changeMode(){
        if(freeMode){
            FreeModeOff();
        } 
        else{
            FreeModeOn();
        }
    }
}