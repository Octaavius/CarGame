using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    private CarScript carScript;
    public AudioClip idleSound;
    public AudioClip lowOn;
    public AudioClip lowOff;

    private float topSpeed = 20f;
    
    private bool startNewAudio = true;
    private float pitch = 1;
    private int mode = 1;

    public bool isStaed = true;
    public bool wasStaed = true;

    private float prevStay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();    
        carScript = GetComponent<CarScript>();
        audioSource.volume = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().soundEffects;
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = rb.velocity.magnitude * 3.6f; 
        pitch = Mathf.Lerp(0, 1f, currentSpeed / topSpeed) * 0.5f;

        if(Mathf.Abs(currentSpeed) < 0.1f && !wasStaed){
            isStaed = true;
            startNewAudio = true;
        }

        if(isStaed && startNewAudio){
            audioSource.clip = idleSound;
            audioSource.pitch  = 1;
            audioSource.Play();
            startNewAudio = false;
            wasStaed = true;
        }

        if((prevStay > 0 && carScript.verticalInput <= 0) || (prevStay <= 0 && carScript.verticalInput > 0)){
            startNewAudio = true;
        }

        if(carScript.verticalInput > 0){
            if(wasStaed){
                startNewAudio = true;
            }
            isStaed = false;
            wasStaed = false;
            if(startNewAudio){
                audioSource.clip = lowOn;
                audioSource.Play();
                startNewAudio = false;
            }
        }
        else{
            if(startNewAudio){
                audioSource.clip = lowOff;
                audioSource.Play();
                startNewAudio = false;
            }
        }
        if(!isStaed) audioSource.pitch = pitch + 0.5f;
        prevStay = carScript.verticalInput;
    } 

    public void topSpeedUp(){
        topSpeed += 20f;
    }

    public void topSpeedDown(){
        topSpeed -= 20f;
    }
}
