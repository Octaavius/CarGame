using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasManager : MonoBehaviour
{
    public void LoadSingle(){
        SceneManager.LoadScene("FirstMap");
    }

    public void LoadMulti(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    
}
