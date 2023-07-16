using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TransparentCode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float transparentModeOn = 1f;
    public float transparentModeOff = 0.5f;

    public void OnPointerDown(PointerEventData eventData){
        Color temp = this.GetComponent<Image>().color;
        temp.a=transparentModeOn;
        this.GetComponent<Image>().color = temp;
    }

    public void OnPointerUp(PointerEventData eventData){
        Color temp = this.GetComponent<Image>().color;
        temp.a = transparentModeOff;
        this.GetComponent<Image>().color = temp;
    }
}
