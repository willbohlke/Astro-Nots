using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        } else if (activateTimer && timeValue < 1) {
            timeText.text = "Go!!!";
            countdownFinished = true;
            activateTimer = false;
            
        } else {
            StartCoroutine(ClearTextAfterDelay(1.0f));
        }


    }

    IEnumerator ClearTextAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        timeText.text = "";
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
