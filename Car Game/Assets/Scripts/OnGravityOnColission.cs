using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGravityOnColission : MonoBehaviour
{
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Car"){
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
