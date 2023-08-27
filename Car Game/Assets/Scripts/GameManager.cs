using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject spawnPoint;
    
    [SerializeField]
    private GameObject currentCar;
    public GameObject carCamera;
    
    private string sceneName; 
    
    [SerializeField]
    private GameObject UI;

    public GameObject[] carList;
    public int lastCarId = 0; 

    void Awake(){
        Application.targetFrameRate = 120;
        
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Game manager is being awaked");
    }

    public void ChangeCar(){
        Destroy(currentCar);
        
        int carId = (carList.Length == lastCarId + 1) ? lastCarId = 0 : ++lastCarId;

        spawnCarFromCarPark(carId);
    }

    void FixedUpdate(){
        if(sceneName != SceneManager.GetActiveScene().name){
            sceneName = SceneManager.GetActiveScene().name;
            if(sceneName != "Menu"){
                UI = GameObject.Find("UI");
                ShowUI();
                carCamera = GameObject.Find("Camera");            
                if(carCamera)
                    carCamera.GetComponent<Camera>().enabled = true;
                
                spawnPoint = GameObject.Find("SpawnPoint");
                spawnCarFromCarPark(lastCarId);
            }
        }
    }

    public void spawnCarFromCarPark(int carId){
        currentCar = Instantiate(carList[carId], spawnPoint.transform.position, Quaternion.identity);
        Debug.Log("Instantiated");

        Follow followScript = carCamera.GetComponent<Follow>();
        followScript.Target = currentCar;
        followScript.setCameraPosition(currentCar.transform.Find("Pos1"), currentCar.transform.Find("Pos2"));  
        
        PhoneCameraRotate phoneCameraScript = carCamera.GetComponent<PhoneCameraRotate>();
        phoneCameraScript.Target = currentCar.transform;

        FreeCamera freeCameraScr = carCamera.GetComponent<FreeCamera>();
        freeCameraScr.Target = currentCar.transform;

        Debug.Log("tachometr set");
        Tachometer tachometerScr = UI.transform.Find("Canvas/meters/Tachometer").GetComponent<Tachometer>();
        tachometerScr.carScript = currentCar.GetComponent<CarScript>();

        Debug.Log("speedometr set");
        Speedometer speedometerScr = UI.transform.Find("Canvas/meters/Speedometer").GetComponent<Speedometer>();
        speedometerScr.car = currentCar;

        Button reverseButton = UI.transform.Find("Canvas/GearButtons/ReverseButton").GetComponent<Button>();
        reverseButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnReverse());
        reverseButton.onClick.AddListener(() => followScript.changeModeTo2());

        Button driveButton = UI.transform.Find("Canvas/GearButtons/DriveButton").GetComponent<Button>();
        driveButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnDrive());
        driveButton.onClick.AddListener(() => followScript.changeModeTo1());
    }

    public void resetPosition(){
        Rigidbody rb = currentCar.GetComponent<Rigidbody>(); 
        rb.velocity = Vector3.zero;
        currentCar.transform.position = spawnPoint.transform.position; 
    } 
    
    public void GoToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void HideUI(){
        UI.transform.Find("Canvas").gameObject.SetActive(false);
    }

    public void ShowUI(){
        UI.transform.Find("Canvas").gameObject.SetActive(true);
    }
}
