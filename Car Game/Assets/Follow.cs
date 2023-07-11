using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
   	public Transform Pos1 = null;
    public Transform Pos2 = null;
    public GameObject Target = null;
    private GameObject TargetCopy = null;

	public GameObject cameraPosition = null;
	public float speed = 1.5f;

    public Vector3 offset;

    public static int mode = 1; 
  
    void Start(){
        TargetCopy = new GameObject();
    }

    void FixedUpdate()
    {   
        TargetCopy.transform.position = Target.transform.position + offset;

        this.transform.LookAt(TargetCopy.transform.position);
		float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, cameraPosition.transform.position) * speed);
		this.transform.position = Vector3.MoveTowards(this.transform.position, cameraPosition.transform.position, car_Move * Time.deltaTime);

        if(mode == 1){
            cameraPosition.transform.position = Pos1.position;
        }
        else if(mode == 2){
            cameraPosition.transform.position = Pos2.position;
        }
    }

    public static void changeModeTo1(){
        mode = 1;
    }

    public static void changeModeTo2(){
        mode = 2;
    }   
}