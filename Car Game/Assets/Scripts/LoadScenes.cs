using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadSceneNames(string name){
        SceneManager.LoadScene(name);
    }

    public void SetMode(string mode){
        GameManager gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        switch(mode) 
        {
        case "freeride":
           gm.gameMode = GameMode.FreeRide;
           break;
        case "timeatack":
            gm.gameMode = GameMode.TimeAtack;
            break;
        }

    }
}
