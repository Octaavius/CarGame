using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject firstMenu;
    public GameObject secondMenu;
    public GameObject secondMenuForMultiplayer;

    public void Awake(){
        Button playButton = secondMenu.transform.Find("Play button").GetComponent<Button>();
        playButton.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<SceneManagerScript>().LoadScene());
    }

    public void LoadSingle(){
        SceneManager.LoadScene("FirstMap");
    }

    public void LoadMulti(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    public void SwitchMenuToSecondMenu(){
        secondMenu.SetActive(true);
        firstMenu.SetActive(false);
    }

    public void SwitchMenuToSecondMenuForMultiplayer(){
        secondMenuForMultiplayer.SetActive(true);
        firstMenu.SetActive(false);
    }

    public void returnToMenuFromSecondMenu(){
        firstMenu.SetActive(true);
        secondMenu.SetActive(false);
    }
    
    public void returnToMenuFromSecondMenuForMultiplayer(){
        firstMenu.SetActive(true);
        secondMenuForMultiplayer.SetActive(false);
    }
}
