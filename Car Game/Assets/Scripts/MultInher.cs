using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class MultInher : CarScript
{
    private PhotonView view;

    private GameObject UI;

    public GameObject spawnPoint;

    void Awake(){
        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        view = GetComponent<PhotonView>();
        if (view.Owner.IsLocal){
            UI = GameObject.FindGameObjectWithTag("UI");
            Camera.main.GetComponent<Follow>().Target = gameObject;
            
            brake = GameObject.FindGameObjectWithTag("Brakes").GetComponent<ButtonHoldChec>();
            handBrake = GameObject.FindGameObjectWithTag("handBrakes").GetComponent<ButtonHoldChec>();
  
            PhoneCameraRotate phoneCameraScript = Camera.main.GetComponent<PhoneCameraRotate>();
            phoneCameraScript.Target = gameObject.transform;

            Tachometer tachometerScr = UI.transform.Find("Canvas/meters/Tachometer").GetComponent<Tachometer>();
            tachometerScr.carScript = GetComponent<CarScript>();

            Speedometer speedometerScr = UI.transform.Find("Canvas/meters/Speedometer").GetComponent<Speedometer>();
            speedometerScr.car = gameObject;

            Button reverseButton = UI.transform.Find("Canvas/GearButtons/ReverseButton").GetComponent<Button>();
            reverseButton.onClick.AddListener(() => TurnOnReverse());

            Button driveButton = UI.transform.Find("Canvas/GearButtons/DriveButton").GetComponent<Button>();
            driveButton.onClick.AddListener(() => TurnOnDrive());


            spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            Button resetButton = UI.transform.Find("Canvas/otherButtons/ResetButton").GetComponent<Button>();
            resetButton.onClick.AddListener(() => ResetPosition());

            Button menuButton = UI.transform.Find("Canvas/otherButtons/MenuButton").GetComponent<Button>();
            menuButton.onClick.AddListener(() => GoToMenu());
        }
    }

    private void Start(){
        PhotonNetwork.SendRate = 30;

        PhotonNetwork.SerializationRate = 30;
    }

    public void ResetPosition(){
        Rigidbody rb = GetComponent<Rigidbody>(); 
        rb.velocity = Vector3.zero;
        transform.position = spawnPoint.transform.position; 
    } 

    public void GoToMenu(){
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu");
    }

    void Update() {
        if(view.IsMine){
            base.Update();
        }
    }
}
