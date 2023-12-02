using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
    #if UNITY_EDITOR
    using UnityEditor;
    #endif

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject playerObject;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gravityStatusText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI exitText;

    public float time = 0.0f;
    private GameObject mainCameraObj;
    private GameObject platObj;
    public float distTraveled = 0.0f;
    public float score = 0.0f;

    private bool gameStarted = false;
    private bool gameOver = false;
    public float grav = 0.0f;
    private Vector3 startPos;



    //private float score = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

        time = UnityEngine.Random.Range(8.0f, 16.0f);
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        platObj = GameObject.FindGameObjectWithTag("Ground");
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
        {
            Debug.LogError("playerObj is not initialized");
            return;
        }
        else
        {
            startPos = playerObject.transform.position;
        }

        PlayerController playerController = playerObject.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component is not found on playerObj");
            return;
        }
        else
        {
            gameStarted = playerController.countdownFinished;
            gameOver = playerController.isOnGround;
            grav = playerController.gravityMultiplier;
        }



    }

    // Update is called once per frame
    void Update()
    {
        gameStarted = playerController.countdownFinished;
        gameOver = playerController.isOnGround;
        grav = playerController.gravityMultiplier;

        //Debug.Log(playerController.countdownFinished);
        float distanceFromStart = startPos.y - playerObject.transform.position.y;
        DynamicCamera(distanceFromStart);

        if (gameStarted && !gameOver)
        {
            time -= Time.deltaTime;
            string formattedTime1 = time.ToString("F0");
            timerText.text = formattedTime1;
        }
        else if (gameOver)
        {
            restartText.text = "Restart?";
            exitText.text = "Exit.";
            score = time;
            string formattedTime2 = time.ToString("F2");
            timerText.text = formattedTime2;
            CalculateScore(score);
        }

        DisplayGravity();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
        {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
        }
    // return the y value that's between the player and the platform
    float GetYPoint()
    {
        float y1 = playerObject.transform.position.y;
        float y2 = platObj.transform.position.y;

        float y3 = (y1 + y2) / 2;
        return y3;
    }

    void DisplayGravity()
    {

        if (grav >= 0.75f)
        {
            gravityStatusText.color = Color.red;
            gravityStatusText.text = "Gravity: Strong";
        }
        else if (grav >= 0.5f)
        {
            gravityStatusText.color = Color.yellow;
            gravityStatusText.text = "Gravity: Standard";
        }
        else
        {
            gravityStatusText.color = Color.cyan;
            gravityStatusText.text = "Gravity: Weak";
        }

    }

    // move the camera based on the player's y position
    void DynamicCamera(float dis)
    {
        float zoom = -20 + dis * 0.5f;
        float Yval = 30 - dis * 1.5f;

        mainCameraObj.transform.position = new Vector3(0, Yval, zoom);
        mainCameraObj.transform.LookAt(playerObject.transform);

    }
    /*
        void ShakeCamera(float amount) {
            float x = Random.Range(-1f, 1f) * amount;
            float y = Random.Range(-1f, 1f) * amount;

            mainCameraObj.transform.position = new Vector3(x, y, 0);
        }
    */
    void CalculateScore(float finalTime)
    {
        if (finalTime < 0.01f && finalTime > -0.01f)
        {
            timerText.color = Color.yellow;
            //Debug.Log("PERFECT SCORE!!!");
        }
        else if (finalTime < 0.5f && finalTime > -0.5f)
        {
            timerText.color = Color.green;
            //Debug.Log("Great score!");
        }
        else if (finalTime < 2.0f && finalTime > -2.0f)
        {
            timerText.color = Color.gray;
            //Debug.Log("Avg score!");
        }
        else
        {
            timerText.color = Color.red;
            //Debug.Log("oof...");
        }
    }

}
