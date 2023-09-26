using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HSVPicker;

public enum GameMode{
    FreeRide,
    TimeAtack
}

public class GameManager : MonoBehaviour
{
    private GameObject spawnPoint;
    
    private GameObject currentCar;
    private GameObject carCamera;
    
    public string sceneName; 
    private GameObject UI;

    // [HideInInspector]
    public GameObject[] carList;
    [HideInInspector]
    public int lastCarId = 0; 

    private ColorPicker picker;
    [HideInInspector]
    public Color[] carsColor;

    public GameObject steeringWheel;
    public GameObject arrows;

    [HideInInspector]
    public string controllerType = "arrows";

    [HideInInspector]
    public GameMode gameMode;

    void Awake(){
        Application.targetFrameRate = 120;
        
        sceneName = SceneManager.GetActiveScene().name;

        carsColor = new Color[carList.Length]; 
    }

    void Start(){
        for(int i = 0; i < carList.Length; i++){
            carsColor[i] = carList[i].GetComponent<UpdateColorScript>().renderer[0].sharedMaterial.color;
        }
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
        currentCar = Instantiate(carList[carId], spawnPoint.transform.position, spawnPoint.transform.rotation);
        //currentCar.GetComponent<CarScript>().enabled = true;

        Follow followScript = carCamera.GetComponent<Follow>();
        followScript.Target = currentCar;
        followScript.setCameraPosition(currentCar.transform.Find("Pos1"), currentCar.transform.Find("Pos2"));  
        
        PhoneCameraRotate phoneCameraScript = carCamera.GetComponent<PhoneCameraRotate>();
        phoneCameraScript.Target = currentCar.transform;
        Button cameraButton = UI.transform.Find("Canvas/otherButtons/SwitchCamera").GetComponent<Button>();
        cameraButton.onClick.AddListener(() => this.enableFreeCamera());

        FreeCamera freeCameraScr = carCamera.GetComponent<FreeCamera>();
        freeCameraScr.Target = currentCar.transform;

        Tachometer tachometerScr = UI.transform.Find("Canvas/meters/Tachometer").GetComponent<Tachometer>();
        tachometerScr.carScript = currentCar.GetComponent<CarScript>();

        Speedometer speedometerScr = UI.transform.Find("Canvas/meters/Speedometer").GetComponent<Speedometer>();
        speedometerScr.car = currentCar;

        Button reverseButton = UI.transform.Find("Canvas/GearButtons/ReverseButton").GetComponent<Button>();
        reverseButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnReverse());
        reverseButton.onClick.AddListener(() => followScript.changeModeTo2());

        Button driveButton = UI.transform.Find("Canvas/GearButtons/DriveButton").GetComponent<Button>();
        driveButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnDrive());
        driveButton.onClick.AddListener(() => followScript.changeModeTo1());

        if(gameMode == GameMode.TimeAtack){
            StartTime();
            TimeAtackManager TAM = UI.transform.Find("TimeAtackPanel").GetComponent<TimeAtackManager>();
        }
    }

    public void resetPosition(){
        Rigidbody rb = currentCar.GetComponent<Rigidbody>(); 
        rb.velocity = Vector3.zero;
        currentCar.transform.position = spawnPoint.transform.position;
        currentCar.transform.eulerAngles = new Vector3(0f, currentCar.transform.eulerAngles.y, 0f);
    } 
    
    public void enableFreeCamera(){
        PhoneCameraRotate phoneCameraScript = carCamera.GetComponent<PhoneCameraRotate>();
        Debug.Log("asdasd");
        phoneCameraScript.enabled = !phoneCameraScript.enabled;
    }

    public void GoToMenu(){
        HideTime();
        SceneManager.LoadScene("Menu");
    }

    public void HideUI(){
        UI.transform.Find("Canvas").gameObject.SetActive(false);
    }

    public void ShowUI(){
        UI.transform.Find("Canvas").gameObject.SetActive(true);
    }

    public void UpdateColor(){
        picker = GameObject.FindGameObjectWithTag("ColorPicker").GetComponent<ColorPicker>();
        carsColor[lastCarId] = picker.CurrentColor;
    }

    public void SetArrowsController(){
        steeringWheel.SetActive(false);
        arrows.SetActive(true);
        controllerType = "arrows";
    }

    public void SetWheelController(){
        steeringWheel.SetActive(true);
        arrows.SetActive(false);
        controllerType = "wheel";
    }

    private void StartTime(){
        UI.transform.Find("TimeAtackPanel").gameObject.SetActive(true);
    }

    public void HideTime(){
        UI.transform.Find("TimeAtackPanel").gameObject.SetActive(false);
        TimeAtackManager TAM = UI.transform.Find("TimeAtackPanel").GetComponent<TimeAtackManager>();
        TAM.ResetStopwatch();
    }
}
