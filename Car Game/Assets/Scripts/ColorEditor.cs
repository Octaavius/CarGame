using HSVPicker;
using UnityEngine;
namespace HSVPickerExamples
{
    public class ColorEditor : MonoBehaviour 
    {
        public Renderer[] renderer;
        private ColorPicker picker;

        private Color Color;

	    // Use this for initialization
	    void Start () 
        {
            picker = GameObject.FindGameObjectWithTag("ColorPicker").GetComponent<ColorPicker>();
            Color = renderer[0].material.color;
            picker.onValueChanged.AddListener(color =>
            {
                for(int n = 0; n < renderer.Length; n++){
                    renderer[n].material.color = color;
                }
                Color = color;
            });

		    picker.CurrentColor = Color;
        }
	
	    // Update is called once per frame
	    void Update () {
	
	    }
    }
}