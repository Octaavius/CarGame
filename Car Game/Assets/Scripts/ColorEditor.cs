using HSVPicker;
using UnityEngine;
namespace HSVPickerExamples
{
    public class ColorEditor : MonoBehaviour 
    {
        public Renderer[] renderer;
        private ColorPicker picker;

        private Color color;
        private GameManager gm;

        private bool wasFound = false;

	    // Use this for initialization
	    void findPicker() 
        {
            gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
            color = renderer[0].material.color;
            if (GameObject.FindGameObjectWithTag("ColorPicker"))
            {
                Debug.Log("FIND");
                picker = GameObject.FindGameObjectWithTag("ColorPicker").GetComponent<ColorPicker>();
                picker.onValueChanged.AddListener(newColor =>
                {
                    changeColor(color);
                    color = newColor;
                    gm.carsColor[gm.lastCarId] = color;
                });

                picker.CurrentColor = color;
            }
        }

        public void changeColor(Color color){
            for(int n = 0; n < renderer.Length; n++){
                renderer[n].material.color = color;
            }
        }

        public void Update(){
            if(!wasFound && GameObject.FindGameObjectWithTag("ColorPicker")){
                findPicker();
                wasFound = true;
            }
        }
    }
}