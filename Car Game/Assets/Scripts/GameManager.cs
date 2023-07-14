using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject carParkObj;
    public GameObject carCamera;
    public GameObject currentCar;
    public GameObject UI;

    void Start(){
        CarPark carParkScr = carParkObj.GetComponent<CarPark>();
        currentCar = Instantiate(currentCar, spawnPoint.transform.position, Quaternion.identity);
        
        Follow followScript = carCamera.GetComponent<Follow>();
        followScript.Target = currentCar;
        followScript.setCameraPosition(currentCar.transform.Find("Pos1"), currentCar.transform.Find("Pos2"));  
        
        PhoneCameraRotate phoneCameraScript = carCamera.GetComponent<PhoneCameraRotate>();
        phoneCameraScript.Target = currentCar.transform;

        Tachometer tachometerScr = UI.transform.Find("Canvas/meters/Tachometer").GetComponent<Tachometer>();
        tachometerScr.car = currentCar;

        Speedometer speedometerScr = UI.transform.Find("Canvas/meters/Speedometer").GetComponent<Speedometer>();
        speedometerScr.car = currentCar;

        Button reverseButton = UI.transform.Find("Canvas/GearButtons/ReverseButton").GetComponent<Button>();
        reverseButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnReverse());

        Button driveButton = UI.transform.Find("Canvas/GearButtons/DriveButton").GetComponent<Button>();
        driveButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnDrive());
    }

    public void ChangeCar(){
        Destroy(currentCar);
        CarPark carParkScr = carParkObj.GetComponent<CarPark>();
        
        int carId = (carParkScr.carList.Length == carParkScr.lastCarId + 1) ? carParkScr.lastCarId = 0 : ++carParkScr.lastCarId;

        currentCar = Instantiate(carParkScr.carList[carId], spawnPoint.transform.position, Quaternion.identity);
        
        Follow followScript = carCamera.GetComponent<Follow>();
        followScript.Target = currentCar;
        followScript.setCameraPosition(currentCar.transform.Find("Pos1"), currentCar.transform.Find("Pos2"));  
        
        PhoneCameraRotate phoneCameraScript = carCamera.GetComponent<PhoneCameraRotate>();
        phoneCameraScript.Target = currentCar.transform;

        Tachometer tachometerScr = UI.transform.Find("Canvas/meters/Tachometer").GetComponent<Tachometer>();
        tachometerScr.car = currentCar;

        Speedometer speedometerScr = UI.transform.Find("Canvas/meters/Speedometer").GetComponent<Speedometer>();
        speedometerScr.car = currentCar;

        Button reverseButton = UI.transform.Find("Canvas/GearButtons/ReverseButton").GetComponent<Button>();
        reverseButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnReverse());

        Button driveButton = UI.transform.Find("Canvas/GearButtons/DriveButton").GetComponent<Button>();
        driveButton.onClick.AddListener(() => currentCar.GetComponent<CarScript>().TurnOnDrive());
    }

    public void resetPosition(){
        Rigidbody rb = currentCar.GetComponent<Rigidbody>(); 
        rb.velocity = Vector3.zero;
        currentCar.transform.position = spawnPoint.transform.position; 
    } 
}
