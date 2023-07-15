using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsController : MonoBehaviour
{
    public GameObject[] buttons;

    public void activeOthersButtons(bool mode){
        buttons[0].SetActive(!mode);
        for(int i = 1; i < buttons.Length; i++){
            buttons[i].SetActive(mode);
        }
    }
}
