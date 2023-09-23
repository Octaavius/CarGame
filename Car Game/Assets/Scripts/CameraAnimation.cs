using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HSVPickerExamples
{

public class CameraAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private MenuManager menuScript;

    //public GameObject[] carList;

    private GameObject currentCar = null;

    public GameObject spawnPoint;

    private bool returnToMenuBool = true;

    private GameManager gm;
    
    void Start(){
        gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
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
        if(!returnToMenuBool){
            currentCar = Instantiate(menuScript.carList[gm.lastCarId], spawnPoint.transform.position, spawnPoint.transform.rotation);
            currentCar.GetComponent<ColorEditor>().changeColor(gm.carsColor[gm.lastCarId]);
        }
    }

    public void returnToMenu(){
        returnToMenuBool = true;
    }
    // public void StartAnimation(){
    //     animator.SetTrigger("TrUp");
    // }
}
}
