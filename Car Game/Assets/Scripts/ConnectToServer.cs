using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class ConnectToServer : MonoBehaviourPunCallbacks
{

    public Button[] joinButtons;
    // Start is called before the first frame update
    public void Start()
    {
        buttonsActivve(false);
        PhotonNetwork.ConnectUsingSettings();
        OnConnectedToMaster();
    }

    void Update(){
        if (!PhotonNetwork.IsConnected){
            buttonsActivve(false);
        }
    }

    private void buttonsActivve(bool state){
        for(int i = 0; i < joinButtons.Length; i++){
            joinButtons[i].interactable = state;
        }
    }

    public override void OnConnectedToMaster(){
        buttonsActivve(true);
    }
}
