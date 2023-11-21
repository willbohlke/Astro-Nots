using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1.25f;
    public float gravityMultiplier = 2.0f;
    public bool isOnGround = false;

    private Vector3 startPos;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = false;
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        // make player thrust upward if spacebar is pressed and they havent touched the ground yet
        if (!isOnGround) {
            playerRb.AddForce(Vector3.up * thrustForce * verticalInput);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            Debug.Log("Game Over!");
        }
    }

}
