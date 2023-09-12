using HSVPicker;
using UnityEngine;
namespace HSVPickerExamples
{
    public class ColorEditor : MonoBehaviour 
    {
        public Renderer[] renderer;
        private ColorPicker picker;

        private Color Color;
        private GameManager gm;

	    // Use this for initialization
	    void Start () 
        {
            gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
            Color = renderer[0].material.color;
            if (GameObject.FindGameObjectWithTag("ColorPicker"))
            {
                picker = GameObject.FindGameObjectWithTag("ColorPicker").GetComponent<ColorPicker>();
                picker.onValueChanged.AddListener(color =>
                {
                    changeColor(color);
                    Color = color;
                    gm.carsColor[gm.lastCarId] = color;
                });

                picker.CurrentColor = Color;
            }
        }

        public void changeColor(Color color){
            for(int n = 0; n < renderer.Length; n++){
                renderer[n].material.color = color;
            }
        }
    }
}