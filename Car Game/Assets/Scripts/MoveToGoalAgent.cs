using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent{

    public CheckPoints checkPoints;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private int gear = 1;

    public override void OnEpisodeBegin(){
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void OnActionReceived(ActionBuffers actions){
        horizontalInput = actions.ContinuousActions[0];
        verticalInput = actions.ContinuousActions[1];
        gear = actions.DiscreteActions[0];

        GetComponent<CarScript>().SetInputs(horizontalInput, verticalInput, gear);
    }

    public override void CollectObservations(VectorSensor sensor){
        Vector3 checkForard = checkPoints.GetNextCheckpoint().forward;
        float directionDot = Vector3.Dot(transform.forward, checkForard);
        sensor.AddObservation(directionDot);
    } 
    
    private void OnTriggerEnter(Collider other){
        if(other.transform == checkPoints.GetNextCheckpoint()){
            AddReward(1f);
            other.gameObject.SetActive(false);
            checkPoints.increaseId();
        }
        else{
            AddReward(-1f);
            EndEpisode();
            checkPoints.ResetCheckPoints();
        }
    }
}
