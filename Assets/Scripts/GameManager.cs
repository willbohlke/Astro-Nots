using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject playerObject;
    public TextMeshProUGUI timerText;
    public float time = 12.0f;

    private bool gameStarted = false;
    private bool gameOver = false;
    private float score = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //always check for null reference exception
        if (playerController != null) {
            gameStarted = playerController.countdownFinished;
            gameOver = playerController.isOnGround;
        }
        
        if (gameStarted && !gameOver) {
            time -= Time.deltaTime;
            string formattedTime1 = time.ToString("F0");
            timerText.text = formattedTime1;
        } else if (gameOver) {
            score = time;
            string formattedTime2 = time.ToString("F2");
            timerText.text = formattedTime2;
        }
    }

    void DisplayTime(float timeToDisplay) {

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 1000;

        timerText.text = string.Format("{0:0}", seconds);
    }
}
