using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasManager : MonoBehaviour
{
    public void LoadSimpleGameMode(){
        SceneManager.LoadScene("S1");
    }

    public void MultiMenuLoad(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }
}
