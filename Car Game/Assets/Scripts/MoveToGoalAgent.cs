using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent{

    public Transform targetTransform;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private int gear = 1;

    public override void OnEpisodeBegin(){
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void OnActionReceived(ActionBuffers actions){
        horizontalInput = actions.ContinuousActions[0];
        verticalInput = actions.ContinuousActions[1];
        gear = actions.DiscreteActions[0];

        GetComponent<CarScript>().SetInputs(horizontalInput, verticalInput, gear);
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(transform.localRotation);
    } 
    
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Finish") SetReward(1f);  //AddReward
        else SetReward(-1f);
        EndEpisode();
    }

    
}
