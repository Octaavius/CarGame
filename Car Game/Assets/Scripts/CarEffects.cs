using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    private WheelCollider CorrespondingCollider;
    public GameObject skidMarkPrefab;
    public ParticleSystem smokePrefab;
    private AudioSource audio;

    private bool wasStart = false;

    private void Start()
    {
        CorrespondingCollider = GetComponent<WheelCollider>();
        audio = GetComponent<AudioSource>();
        skidMarkPrefab.SetActive(false);
        if(smokePrefab) smokePrefab.Stop();
        audio.volume = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().soundEffects;
    }

    private void Update()
    {
        WheelHit correspondingGroundHit;
        CorrespondingCollider.GetGroundHit(out correspondingGroundHit);

        if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 0.5f)
        {
            if(!wasStart){
                skidMarkPrefab.SetActive(true);
                if(smokePrefab) smokePrefab.Play();
                if(audio) audio.Play();
                wasStart = true;
            }
        }
        else if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) <= 0.45f)
        {
            skidMarkPrefab.SetActive(false);
            if(smokePrefab) smokePrefab.Stop();
            if(audio) audio.Stop();
            wasStart = false;
        }
    }
}
