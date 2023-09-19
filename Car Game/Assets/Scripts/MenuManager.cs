using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject firstMenu;
    public GameObject secondMenu;
    public GameObject secondMenuForMultiplayer;
    public GameObject settings;
    public GameObject selectScenePanel;
    private GameObject curActivePanel;
    
    [Header("Car selection settings")]
    public GameObject[] carList;
    public GameObject porsheForMenu;
    public Animator animator;
    public GameObject spawnPoint;
    private GameObject currentCar = null;
    
    [HideInInspector]
    public int lastCarId = 0;

    public void Awake(){
        Button playButton = secondMenu.transform.Find("Play button").GetComponent<Button>();
        playButton.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<SceneManagerScript>().LoadScene());
    }

    public void Start(){
        curActivePanel = firstMenu;
    }

    public void LoadSingle(){
        SceneManager.LoadScene("FirstMap");
    }

    public void LoadMulti(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    public void SwitchMenuToSecondMenu(){
        firstMenu.SetActive(false);
        // porsheForMenu.SetActive(false);
        //Destroy(porsheForMenu);
        StartAnimation();
        secondMenu.SetActive(true);
        curActivePanel = secondMenu;
    }

    public void SwitchMenuToSecondMenuForMultiplayer(){
        firstMenu.SetActive(false);
        //porsheForMenu.SetActive(false);
        secondMenuForMultiplayer.SetActive(true);
        curActivePanel = secondMenu;
    }

    public void returnToMenuFromSecondMenu(){
        secondMenu.SetActive(false);
        StartAnimation();
        firstMenu.SetActive(true);
        curActivePanel = firstMenu;
    }
    
    public void returnToMenuFromSecondMenuForMultiplayer(){
        secondMenuForMultiplayer.SetActive(false);
        //porsheForMenu.SetActive(true);
        firstMenu.SetActive(true);
        curActivePanel = firstMenu;
    }
    
    // public void ResetValue(){
    //     animator.ResetTrigger("TrUp");
    //     if(currentCar){
    //         Debug.Log(currentCar);
    //         Destroy(currentCar);
    //         Debug.Log("destrou car");
    //     }
    //     else{
    //         returnToMenuBool = false;
    //         Debug.Log(returnToMenuBool);
    //     }
    //     if(!returnToMenuBool)
    //         currentCar = Instantiate(menuScript.carList[menuScript.lastCarId], spawnPoint.transform.position, spawnPoint.transform.rotation);
    // }
    
    public void StartAnimation(){
        animator.SetTrigger("TrUp");
    }
    // public void showCarInMenu(){
    //     carForSecondMenu.SetActive(true);
    // }
    // public void hideCarInMenu(){
    //     carForSecondMenu.SetActive(false);
    // }

    public void openSettings(){
        settings.SetActive(true);
        curActivePanel.SetActive(false); 
    }
    
    public void closeSettings(){
        settings.SetActive(false);
        curActivePanel.SetActive(true); 
    }

    public void SelectMapPanel(){
        curActivePanel.SetActive(false);
        selectScenePanel.SetActive(true);
        curActivePanel = selectScenePanel;
    } 
}
