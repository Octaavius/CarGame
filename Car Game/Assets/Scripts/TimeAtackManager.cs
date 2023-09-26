using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAtackManager : MonoBehaviour
{
    public Text timerText;
    public Text endTimerText;
    public int checkerCount = 0;
    private float startTime;
    private bool isRunning = false;

    public GameObject endPanel;

    void Start()
    {
        timerText.text = "0:00.00";
    }

    void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            string minutes = ((int)elapsedTime / 60).ToString("D1");
            string seconds = (elapsedTime % 60).ToString("F2");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartStopwatch()
    {
        if (!isRunning)
        {
            startTime = Time.time;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public void ResetStopwatch()
    {
        isRunning = false;
        timerText.text = "0:00.00";
    }

    public void endPanelOpne(){
        endPanel.SetActive(true);
        endTimerText.text = "Your time: " + timerText.text;
    }

    public void RestartRace(){
        ResetStopwatch();
        endPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().sceneName = "lol";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu(){
        ResetStopwatch();
        endPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().HideUI();
        GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().GoToMenu();
    }

}

