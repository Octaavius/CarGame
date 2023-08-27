using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ChoiceScript : MonoBehaviour
{   
    public Transform carText;
    public Transform mapText;
    private GameObject gameManager;
    private SceneManagerScript sceneManagerScript;
    private GameManager gameManagerScript;

    private void Awake(){
        gameManager = GameObject.Find("GameManager");
        sceneManagerScript = gameManager.GetComponent<SceneManagerScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    public void nextCar(){
        gameManagerScript.lastCarId++;
        
        if(gameManagerScript.lastCarId == gameManagerScript.carList.Length){
            gameManagerScript.lastCarId = 0;
        }
        carText.GetComponent<Text>().text = gameManagerScript.carList[gameManagerScript.lastCarId].name;
    }
    public void prevCar(){
        gameManagerScript.lastCarId--;
        
        if(gameManagerScript.lastCarId == -1){
            gameManagerScript.lastCarId = gameManagerScript.carList.Length - 1;
        }
        carText.GetComponent<Text>().text = gameManagerScript.carList[gameManagerScript.lastCarId].name;
    }
    public void nextMap(){
        sceneManagerScript.currentMap++;
        
        if(sceneManagerScript.currentMap == sceneManagerScript.mapNames.Length){
            sceneManagerScript.currentMap = 0;
        }
        
        mapText.GetComponent<Text>().text = sceneManagerScript.mapNames[sceneManagerScript.currentMap];
    }
    public void prevMap(){
        sceneManagerScript.currentMap--;
        if(sceneManagerScript.currentMap == -1){
            sceneManagerScript.currentMap = sceneManagerScript.mapNames.Length - 1;
        }
        mapText.GetComponent<Text>().text = sceneManagerScript.mapNames[sceneManagerScript.currentMap];
    }
}
