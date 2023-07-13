using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPark : MonoBehaviour
{   
    public GameObject[] carList;
    [HideInInspector]
    public int lastCarId;

    void Start(){
        lastCarId = 0; 
    }
}
