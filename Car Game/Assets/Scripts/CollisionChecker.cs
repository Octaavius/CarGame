using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool firstTrigger = false;
    [HideInInspector]
    public bool wasTrigger = false;
    private bool stopWatch = false; 

    public void collider(GameObject col){
        if(col.tag == "Car" && !wasTrigger){
            
            if(firstTrigger && !wasTrigger){
                GameObject.FindGameObjectWithTag("TimeAtackPanel").GetComponent<TimeAtackManager>().StartStopwatch();
            }
            
            wasTrigger = true;
        }

        else if(col.gameObject.tag == "Car" && checkEnd() && !stopWatch){
            TimeAtackManager timeManager = GameObject.FindGameObjectWithTag("TimeAtackPanel").GetComponent<TimeAtackManager>();
            timeManager.StartStopwatch();
            timeManager.endPanelOpne();
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
