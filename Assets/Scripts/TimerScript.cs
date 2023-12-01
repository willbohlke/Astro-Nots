using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeValue = 0f;
    public Text timeText;
    public bool countdownFinished = false;

    private bool activateTimer = false;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        if (activateTimer && timeValue > 1) {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
        } else {
            timeText.text = "Go!";
            timeValue = 0;
            countdownFinished = true;
            activateTimer = false;
        }


    }

    public bool GetCountdownFinished() {
        return countdownFinished;
    }

    void StartCountdown() {
        activateTimer = true;
        timeValue = 3.99f;
    }

    void DisplayTime(float timeToDisplay) {
        if(timeToDisplay < 0) {
            timeToDisplay = 0;
        }

        //float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("{0:0}", seconds);
    }
}
