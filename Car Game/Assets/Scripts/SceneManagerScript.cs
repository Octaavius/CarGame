using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string[] mapNames;
    private int currentMap = 1;

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene(){
        SceneManager.LoadScene(mapNames[currentMap++]);
        
        if(currentMap == mapNames.Length)
            currentMap = 0;
    }

    public void LoadSimpleGameMode(){
        SceneManager.LoadScene("S1");
    }

    public void MultiMenuLoad(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }
}
