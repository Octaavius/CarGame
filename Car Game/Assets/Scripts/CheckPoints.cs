using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public Transform[] checkPoints;
    private int curCheckPointId = 0;

    private int size = 0;

    // Start is called before the first frame update
    void Awake()
    {
        bool skipFirst = true;
        checkPoints = new Transform[gameObject.GetComponentsInChildren<Transform>().Length];
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>()){
            if(size == 0 && skipFirst){
                skipFirst = false;
                continue;
            }
            checkPoints[size++] = child;
        }
            
    }

    // Update is called once per frame
    public Transform GetNextCheckpoint(){
        return checkPoints[curCheckPointId];
    }

    public void increaseId(){
        if(curCheckPointId == size + 1){
            curCheckPointId  = 0;
            return;
        }
        curCheckPointId++;
    }

    public void ResetCheckPoints(){
        curCheckPointId = 0;
        foreach (Transform checkpoint in checkPoints){
            checkpoint.gameObject.SetActive(true);
        }
    }
}
