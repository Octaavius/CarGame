using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAtackManager : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isRunning = false;

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
}

