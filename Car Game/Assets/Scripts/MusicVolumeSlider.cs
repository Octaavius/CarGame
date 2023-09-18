using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicVolumeSlider : MonoBehaviour
{
    private AudioSource audio;
    private Slider sl;

    // Start is called before the first frame update
    void Start()
    {
        sl = gameObject.GetComponent<Slider>();
        updateAudioListner();
        sl.onValueChanged.AddListener((float value) => audio.volume = sl.value);
    }

    public void updateAudioListner(){
        audio = GameObject.FindGameObjectWithTag("musicManager").GetComponent<AudioSource>();
        sl.value = audio.volume;
    }
}
