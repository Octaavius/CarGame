using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private GameObject secondMenu;

    public void Awake(){
        secondMenu = this.transform.Find("SecondMenu").gameObject;    
    }

    public void LoadSingle(){
        SceneManager.LoadScene("FirstMap");
    }

    public void LoadMulti(){
        SceneManager.LoadScene("LoadingMultiplayerScene");
    }

    public void SwitchMenus(){
        secondMenu.SetActive(true);
        Button button = secondMenu.transform.Find("Car button/Left button").GetComponent<Button>();
        button.onClick.AddListener(() => secondMenu.GetComponent<ChoiceScript>().prevCar());

        button = secondMenu.transform.Find("Car button/Right button").GetComponent<Button>();
        button.onClick.AddListener(() => secondMenu.GetComponent<ChoiceScript>().nextCar());
        
        button = secondMenu.transform.Find("Map button/Left button").GetComponent<Button>();
        button.onClick.AddListener(() => secondMenu.GetComponent<ChoiceScript>().prevMap());

        button = secondMenu.transform.Find("Map button/Right button").GetComponent<Button>();
        button.onClick.AddListener(() => secondMenu.GetComponent<ChoiceScript>().nextMap());
        this.transform.Find("FirstMenu").gameObject.SetActive(false);

        button = secondMenu.transform.Find("Play button").GetComponent<Button>();
        button.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<SceneManagerScript>().LoadScene());
    }

}
