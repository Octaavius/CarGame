using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string[] mapNames;
    [HideInInspector]
    public int currentMap = 0;

    public void LoadScene(){
        SceneManager.LoadScene(mapNames[currentMap]);
    }

    public void LoadNextScene(){
        currentMap = (currentMap == mapNames.Length - 1) ? 0 : currentMap + 1; 

        SceneManager.LoadScene(mapNames[currentMap]);
    }

    public void MultiMenuLoad(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
}
