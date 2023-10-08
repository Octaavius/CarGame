using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class driftAssistant : MonoBehaviour
{
    [SerializeField]
    private CarScript carScript;
    [SerializeField]
    private Text frwSlip;
    [SerializeField]
    private Text sidewaySlip;
    private void Update()
    {
        WheelHit correspondingGroundHit;
        carScript.wheels[2].GetGroundHit(out correspondingGroundHit);

        frwSlip.text = Mathf.Abs(correspondingGroundHit.forwardSlip).ToString();
        sidewaySlip.text = Mathf.Abs(correspondingGroundHit.sidewaysSlip).ToString();
    }
    /*void showForwardSlip()
    {
        WheelHit correspondingGroundHit;
        carScript.wheels[0].GetGroundHit(out correspondingGroundHit);

        sidewaySlip.text = Mathf.Abs(correspondingGroundHit.forwardSlip).ToString();
    }
    void showSidewaySlip()
    {
        WheelHit correspondingGroundHit;
        carScript.wheels[0].GetGroundHit(out correspondingGroundHit);

        sidewaySlip.text = Mathf.Abs(correspondingGroundHit.sidewaysSlip).ToString();
    }*/
}
