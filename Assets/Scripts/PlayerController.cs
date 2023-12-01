using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1.75f;
    public float gravityMultiplier = 0.0f;
    public float timeValue = 0f;
    public bool isOnGround = false;
    public bool countdownFinished = false;
    public ParticleSystem JetPackThrust;

    private Vector3 startPos;
    private Rigidbody playerRb;
    private float yBound = 25.0f;
    private Animator playerAnim;
    private bool thrusting = false;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = false;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        gravityMultiplier = UnityEngine.Random.Range(0.25f, 1.0f);
        Physics.gravity *= gravityMultiplier;
        StartCoroutine(CountdownRoutine());
        playerRb.useGravity = false;

        if (JetPackThrust != null) {
            JetPackThrust.Stop();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Thrusting: " + thrusting);
        
        if (countdownFinished) {
            playerRb.useGravity = true;
        }

        float verticalInput = Input.GetAxis("Vertical");

        // make player thrust upward if up arrow is pressed and they havent touched the ground yet
            
            if (Input.GetKey(KeyCode.UpArrow) && countdownFinished && !isOnGround) {
                playerRb.AddForce(Vector3.up * thrustForce * verticalInput);
                thrusting = true;
                //Debug.Log("Thrusting!");
            } else {
                //Debug.Log("Not thrusting!");
                thrusting = false;
            }
        
        if (thrusting) {
            if (JetPackThrust != null) {
                JetPackThrust.Play();
            }
        } else {
            if (JetPackThrust != null) {
                JetPackThrust.Stop();
            }
        }

        // constrain player to not go over a certain height
        if (transform.position.y > yBound) {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
            playerRb.velocity = Vector3.zero; // reset velocity so player doesn't appear stuck in midair
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            JetPackThrust.Stop();
            //Debug.Log("Game Over!");
        }
    }

    public bool GetCountdownFinished() {
        return countdownFinished;
    }

    IEnumerator CountdownRoutine() {
        timeValue = 3.0f;
        yield return new WaitForSeconds(timeValue);
        countdownFinished = true;
    }

}
