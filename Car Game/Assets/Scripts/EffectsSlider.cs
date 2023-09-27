using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsSlider : MonoBehaviour
{
    private GameManager gm;
    private Slider sl;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        sl = gameObject.GetComponent<Slider>();
        updateAudioListner();
        sl.onValueChanged.AddListener((float value) => gm.soundEffects = sl.value);
    }

    public void updateAudioListner(){
        sl.value =  gm.soundEffects;
    }
}
