using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    private CarScript carScript;
    public AudioClip[] audiosByGears;
    public float[] timeForGear;
    private AudioClip curAudio;
    
    private bool startNewAudio = true;
    private float audioLength;
    private float pitch = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();    
        carScript = GetComponent<CarScript>();
        curAudio = audiosByGears[0];
        audioSource.clip = curAudio;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = rb.velocity.magnitude * 3.6f;
        float topSpeed = 100f;
        pitch = Mathf.Lerp(0, 1f, currentSpeed / topSpeed);
        audioSource.pitch = pitch * 0.5f + 0.5f;
        
    }

    private float pitchCount(float length){
        if(carScript.gear < 3){
            audioSource.volume = 0.6f;
            return pitch = length / 2f ;
        }
        else if(carScript.gear < 6){
            audioSource.volume = 0.8f;
            return pitch = length /3f;
        }
        audioSource.volume = 1;
        return length / 4f ;
    } 
}
