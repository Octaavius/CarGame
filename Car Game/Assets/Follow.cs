using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
   	public Transform Pos1 = null;
    public Transform Pos2 = null;
    public GameObject Target = null;
	public GameObject T = null;
	public float speed = 1.5f;

    public static int mode = 1; 

  
    void FixedUpdate()
    {
		this.transform.LookAt(Target.transform);
		float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
		this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.deltaTime);

        if(mode == 1){
            T.transform.position = Pos1.position;
        }
        else if(mode == 2){
            T.transform.position = Pos2.position;
        }
    }

    public static void changeModeTo1(){
        mode = 1;
    }

    public static void changeModeTo2(){
        mode = 2;
    }
}