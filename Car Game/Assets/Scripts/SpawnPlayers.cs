using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, spawnPoint.transform.position, Quaternion.identity);
    }
}
