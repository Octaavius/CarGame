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
        SceneManager.LoadScene(mapNames[currentMap++]);
        
        if(currentMap == mapNames.Length)
            currentMap = 0;
    }

    public void MultiMenuLoad(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
}
