using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private MenuManager menuScript;

    //public GameObject[] carList;

    private GameObject currentCar = null;

    public GameObject spawnPoint;

    private bool returnToMenuBool = true;
    
    void Start(){
        animator = GetComponent<Animator>();
        currentCar = GameObject.FindWithTag("Car");
    }

    public void ResetValue(){
        animator.ResetTrigger("TrUp");
        if(currentCar){
            Debug.Log(currentCar);
            Destroy(currentCar);
            Debug.Log("destrou car");
        }
        else{
            returnToMenuBool = false;
            Debug.Log(returnToMenuBool);
        }
        if(!returnToMenuBool)
            currentCar = Instantiate(menuScript.carList[menuScript.lastCarId], spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    public void returnToMenu(){
        returnToMenuBool = true;
    }
    // public void StartAnimation(){
    //     animator.SetTrigger("TrUp");
    // }
}
