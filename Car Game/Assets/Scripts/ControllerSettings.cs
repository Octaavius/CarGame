using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSettings : MonoBehaviour
{
    private GameManager gm;
    public Image arrowsButton;
    public Image stearingWheelButton;

    public void UpdateValues(){
        gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    public void SetWheel(){
        gm.SetWheelController();
        makeTransparent(0.5f, arrowsButton);
        makeTransparent(1f, stearingWheelButton);
    }
    
    public void SetArrows(){
        gm.SetArrowsController();
        makeTransparent(1f, arrowsButton);
        makeTransparent(0.5f, stearingWheelButton);
    }

    private void OnEnable(){
        UpdateValues();
        if(gm.controllerType == "arrows"){
            SetArrows();
        }
        else{
            SetWheel();
        }
    }

    private void makeTransparent(float value, Image src){
        Color color = src.color;

        // Set the alpha component of the color to the desired value
        color.a = value;

        // Apply the new color to the image
        src.color = color;
    }
}
