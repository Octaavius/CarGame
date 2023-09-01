using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{

    private AudioSource audioSource;
    private CarScript carScript;
    public AudioClip[] audiosByGears;
    private bool startNewAudio = true;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
        carScript = GetComponent<CarScript>();
        audioSource.clip = audiosByGears[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(startNewAudio && carScript.verticalInput > 0){
            audioSource.Play();
            startNewAudio = false;
        }
        else if(carScript.verticalInput == 0){
            audioSource.Stop();
            startNewAudio = true;
        }
        
    }
}
