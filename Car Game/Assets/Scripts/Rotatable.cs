using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Rotatable : MonoBehaviour 
{
	[SerializeField] private InputAction pressed, axis;
	
	private Transform cam;
	[SerializeField] private float speed = 1;
	[SerializeField] private bool inverted;
	private Vector2 rotation;
	private bool rotateAllowed;

	public bool xRotation = true;
	public bool yRotation = true;

	public float xStart = 0;
	public float xEnd = Screen.width;
	public float yStart = 0;
	public float yEnd = Screen.height;
	private bool touchIsAllowed = true;

	private void Awake() 
	{
		cam = Camera.main.transform;
		pressed.Enable();
		axis.Enable();
		EnhancedTouchSupport.Enable();
		pressed.performed += _ => { if(this)StartCoroutine(Rotate()); };
		pressed.canceled += _ => { rotateAllowed = false; touchIsAllowed = true;};
		axis.performed += context => { rotation = context.ReadValue<Vector2>(); };	
	}

	private IEnumerator Rotate()
	{	
		rotateAllowed = true;

		while(rotateAllowed)
		{
			if(!touchIsAllowed)
				yield break;
			// apply rotation
			rotation *= speed;
			transform.Rotate(Vector3.up * (inverted? 1: -1), rotation.x * (xRotation ? 1 : 0), Space.World);
			transform.Rotate(cam.right * (inverted? -1: 1), rotation.y * (yRotation ? 1 : 0), Space.World);
			yield return null;
		}
	}

	void Update(){
		foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            // Check if touch position is to the left of the screen center
            if (touch.screenPosition.x > xEnd || touch.screenPosition.x < xStart || touch.screenPosition.y > yEnd || touch.screenPosition.y < xStart)
            {
                Debug.Log("Detected fault!");
				touchIsAllowed = false;
            } else {
				touchIsAllowed = true;
			}
        }
	}
}
