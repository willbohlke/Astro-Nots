using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gravText;
    public float time = 0.0f;
    public float distTraveled = 0.0f;

    private GameObject playerObj;
    private GameObject mainCameraObj;
    private GameObject platObj;
    private Vector3 startPos;
    private bool gameStarted = false;
    private bool gameOver = false;
    private float grav = 0.0f;
    
    //private float score = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(8.0f, 16.0f);
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        platObj = GameObject.FindGameObjectWithTag("Ground");
        playerObj = GameObject.FindGameObjectWithTag("Player");
        startPos = playerObj.transform.position;
        //ShakeCamera(shakeAmount);
        
    }

    // Update is called once per frame
    void Update()
    {

        //always check for null reference exception
        if (playerController != null) {
            gameStarted = playerController.countdownFinished;
            gameOver = playerController.isOnGround;
            grav = playerController.gravityMultiplier;
        }
        
        float distanceFromStart = startPos.y - playerObj.transform.position.y;
        DynamicCamera(distanceFromStart);
        DisplayGravity();
        
        if (gameStarted && !gameOver) {
            time -= Time.deltaTime;
            string formattedTime1 = time.ToString("F0");
            timerText.text = formattedTime1;
        } else if (gameOver) {
            CalculateScore(time);
            string formattedTime2 = time.ToString("F2");
            timerText.text = formattedTime2;
        }
    }

    // return the y value that's between the player and the platform
    float GetYPoint() {
        float y1 = playerObj.transform.position.y;
        float y2 = platObj.transform.position.y;

        float y3 = (y1 + y2) / 2;
        return y3;
    }

    void DisplayGravity() {
        if (grav >= 0.75f) {
            gravText.color = Color.red;
            gravText.text = "Gravity: Strong";
        } else if (grav >= 0.5f) {
            gravText.color = Color.yellow;
            gravText.text = "Gravity: Standard";
        } else  {
            gravText.color = Color.cyan;
            gravText.text = "Gravity: Weak";
        } 
        
    }

    // move the camera based on the player's y position
    void DynamicCamera(float dis) {
        float zoom = -20 + dis*0.5f;
        float Yval = 30 - dis*1.5f;
        
        mainCameraObj.transform.position = new Vector3(0, Yval, zoom);
        mainCameraObj.transform.LookAt(playerObj.transform);

    }
/*
    void ShakeCamera(float amount) {
        float x = Random.Range(-1f, 1f) * amount;
        float y = Random.Range(-1f, 1f) * amount;

        mainCameraObj.transform.position = new Vector3(x, y, 0);
    }
*/
    void CalculateScore(float finalTime) {
        if (finalTime < 0.01f && finalTime > -0.01f) {
            timerText.color = Color.yellow;
            //Debug.Log("PERFECT SCORE!!!");
        } else if (finalTime < 0.5f && finalTime > -0.5f) {
            timerText.color = Color.green;
            //Debug.Log("Great score!");
        } else if (finalTime < 2.0f && finalTime > -2.0f) {
            timerText.color = Color.white;
            //Debug.Log("Avg score!");
        } else {
            timerText.color = Color.red;
            //Debug.Log("oof...");
        }
    }

}
