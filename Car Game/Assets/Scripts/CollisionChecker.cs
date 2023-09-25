using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool firstTrigger = false;
    [HideInInspector]
    public bool wasTrigger = false;
    private bool stopWatch = false; 

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Car" && !wasTrigger){
            if(firstTrigger && !wasTrigger){
                GameObject.FindGameObjectWithTag("TimeAtackPanel").GetComponent<TimeAtackManager>().StartStopwatch();
            }
            
            wasTrigger = true;
        }

        else if(col.gameObject.tag == "Car" && checkEnd() && !stopWatch){
            GameObject.FindGameObjectWithTag("TimeAtackPanel").GetComponent<TimeAtackManager>().StartStopwatch();
            stopWatch = true;
        }
    }

    private bool checkEnd(){
        GameObject[] triggers = GameObject.FindGameObjectsWithTag("TimeAtackTrigger");
        for(int i = 0; i < triggers.Length; i++){
            if(!triggers[i].GetComponent<CollisionChecker>().wasTrigger){
                return false;
            }
        }
        return true;
    }
}
