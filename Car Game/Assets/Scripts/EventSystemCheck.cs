using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemCheck : MonoBehaviour
{
    void Awake(){ 
        if(GameObject.FindGameObjectWithTag("EventSystem") != gameObject){
            Destroy(gameObject);
        }
    }
}
