using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class GasButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;
    private float lowerBorder;
    private float height;
    //[HideInInspector]
    public float verticalModifier;

    void Start()
    {
        lowerBorder = this.transform.position.y - this.GetComponent<RectTransform>().rect.height;
        height = this.GetComponent<RectTransform>().rect.height * 2;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void Update()
    {
        if (isPressed)
        {
            // When the button is pressed, set the axis input based on the touch position
            float touchPositionY = Input.mousePosition.y;
            verticalModifier = Mathf.Clamp((touchPositionY - lowerBorder) / height, 0, 1) ; // Scale the position to your desired input range
        } else
        {
            verticalModifier = 0;
        }
    }
}
