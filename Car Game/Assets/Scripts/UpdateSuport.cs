using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSuport : MonoBehaviour
{
    public WheelCollider collider;
    public CarScript cs;

    private float xRot;
    private float zRot;
    public float yRot = -90;

    public bool updateRotation = true;

    public void Start(){
        xRot = transform.rotation.eulerAngles.x;
        zRot = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        singleSuportUpdate(collider);
    }

    void singleSuportUpdate(WheelCollider collider){
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos; 
        if(updateRotation) transform.transform.localRotation = Quaternion.Euler(new Vector3(xRot, cs.curAngle + yRot, zRot));

    }
}
