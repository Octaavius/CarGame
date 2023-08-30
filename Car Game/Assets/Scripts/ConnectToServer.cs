using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public void tryConnectPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        OnConnectedToMaster();
    }

    public override void OnConnectedToMaster(){
        GetComponent<MenuManager>().SwitchMenuToSecondMenuForMultiplayer();
    }
}
