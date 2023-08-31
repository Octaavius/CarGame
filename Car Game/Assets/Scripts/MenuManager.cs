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
    public GameObject porsheForMenu;
    public GameObject carForSecondMenu;
    public CameraAnimation animator;

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
        firstMenu.SetActive(false);
        //porsheForMenu.SetActive(false);
        //carForSecondMenu.SetActive(true);
        animator.StartAnimation();
        secondMenu.SetActive(true);
    }

    public void SwitchMenuToSecondMenuForMultiplayer(){
        firstMenu.SetActive(false);
        porsheForMenu.SetActive(false);
        carForSecondMenu.SetActive(true);
        secondMenuForMultiplayer.SetActive(true);
    }

    public void returnToMenuFromSecondMenu(){
        secondMenu.SetActive(false);
        carForSecondMenu.SetActive(false);
        porsheForMenu.SetActive(true);
        firstMenu.SetActive(true);
    }
    
    public void returnToMenuFromSecondMenuForMultiplayer(){
        secondMenuForMultiplayer.SetActive(false);
        carForSecondMenu.SetActive(false);
        porsheForMenu.SetActive(true);
        firstMenu.SetActive(true);
    }
    // public void showCarInMenu(){
    //     carForSecondMenu.SetActive(true);
    // }
    // public void hideCarInMenu(){
    //     carForSecondMenu.SetActive(false);
    // }
}
