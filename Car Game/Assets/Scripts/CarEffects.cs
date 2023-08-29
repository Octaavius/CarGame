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
        RaycastHit hit;
        Vector3 colliderCenterPoint = CorrespondingCollider.transform.TransformPoint(CorrespondingCollider.center);

        if (Physics.Raycast(colliderCenterPoint, -CorrespondingCollider.transform.up, out hit,
                            CorrespondingCollider.suspensionDistance + CorrespondingCollider.radius))
        {
            transform.position = hit.point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius);
        }
        else
        {
            transform.position = colliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance);
        }

        WheelHit correspondingGroundHit;
        CorrespondingCollider.GetGroundHit(out correspondingGroundHit);

        if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 0.8f)
        {
            skidMarkPrefab.SetActive(true);
        }
        else if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) <= 0.75f)
        {
            skidMarkPrefab.SetActive(false);
        }
    }
}
