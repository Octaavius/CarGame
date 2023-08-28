using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string[] mapNames;
    public int currentMap = 0;

    public void LoadScene(){
        SceneManager.LoadScene(mapNames[currentMap]);
    }

    public void LoadNextScene(){
        currentMap = (currentMap == mapNames.Length - 1) ? 0 : currentMap + 1; 
        // currentMap++;
        // if(currentMap == mapNames.Length)
        //     currentMap = 0;
        SceneManager.LoadScene(mapNames[currentMap]);
        
    }

    public void MultiMenuLoad(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
}
