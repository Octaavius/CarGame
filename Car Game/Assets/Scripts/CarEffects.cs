using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    public WheelCollider CorrespondingCollider;
    public GameObject skidMarkPrefab;
    public ParticleSystem smokePrefab;

    private bool wasStart = false;

    private void Start()
    {
        skidMarkPrefab.SetActive(false);
        if(smokePrefab) smokePrefab.Stop();
    }

    private void Update()
    {
        WheelHit correspondingGroundHit;
        CorrespondingCollider.GetGroundHit(out correspondingGroundHit);

        if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 0.6f)
        {
            if(!wasStart){
                skidMarkPrefab.SetActive(true);
                if(smokePrefab) smokePrefab.Play();
                wasStart = true;
            }
        }
        else if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) <= 0.55f)
        {
            skidMarkPrefab.SetActive(false);
            if(smokePrefab) smokePrefab.Stop();
            wasStart = false;
        }
    }
}
