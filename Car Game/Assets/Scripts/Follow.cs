using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target = null;
   	
    private Transform Pos1 = null;
    private Transform Pos2 = null;
    private Transform cameraPosition = null;
    private GameObject TargetCopy = null;
	
    public float speed = 1.5f;

    public Vector3 offset;

    public int mode = 1; 
  
    void Start(){
        TargetCopy = new GameObject();
        TargetCopy.name = "CameraTarget";
        cameraPosition = new GameObject().transform;
        cameraPosition.name = "CameraPosition";

        
        Pos1 = Target.transform.Find("Pos1");
        Pos2 = Target.transform.Find("Pos2");
        cameraPosition.position = Pos1.position;
    }

    void FixedUpdate()
    {   
        if(TargetCopy == null){
            TargetCopy = new GameObject();
            TargetCopy.name = "CameraTarget";
            cameraPosition = new GameObject().transform;
            cameraPosition.name = "CameraPosition";
        }
        TargetCopy.transform.position = Target.transform.position + offset;

        this.transform.LookAt(TargetCopy.transform.position);
		float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, cameraPosition.position) * speed);
		this.transform.position = Vector3.MoveTowards(this.transform.position, cameraPosition.position, car_Move * Time.deltaTime);

        if(mode == 1){
            cameraPosition.position = Pos1.position;
        }
        else if(mode == 2){
            cameraPosition.position = Pos2.position;
        }
    }

    public void changeModeTo1(){
        mode = 1;
    }

    public void changeModeTo2(){
        mode = 2;
    }
    public void setCameraPosition(Transform pos1, Transform pos2){
        Pos1 = pos1;
        Pos2 = pos2;
    } 
}