using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffrcts : MonoBehaviour
{
    public WheelCollider CorrespondingCollider;
    public GameObject skidMarkPrefab;

    private void Start()
    {
        skidMarkPrefab.SetActive(false);
    }

    private void Update()
    {
        WheelHit correspondingGroundHit;
        CorrespondingCollider.GetGroundHit(out correspondingGroundHit);

        if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 0.6f)
        {
            skidMarkPrefab.SetActive(true);
        }
        else if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) <= 0.55f)
        {
            skidMarkPrefab.SetActive(false);
        }
    }
}
