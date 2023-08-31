using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator animator;
    private GameManager gm;

    public Vector3 pos;
    public Quaternion rot;

    public GameObject[] carList;

    private GameObject currentCar;
    
    void Start(){
        animator = GetComponent<Animator>();
        gm = GameObject.FindWithTag("gameManager").GetComponent<GameManager>();
        currentCar = GameObject.FindWithTag("Car");
    }

    public void ResetValue(){
        animator.ResetTrigger("TrUp");
        pos = currentCar.transform.position;
        rot = currentCar.transform.rotation;
        Destroy(currentCar);
        currentCar = Instantiate(gm.carList[gm.lastCarId], pos, rot);
    }

    public void StartAnimation(){
        animator.SetTrigger("TrUp");
    }
}
