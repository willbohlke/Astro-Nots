using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 0.5f;
    public Rigidbody playerRB;
    public ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0f)
        {
            playerRB.AddForce(playerRB.transform.up * thrustForce, ForceMode.Impulse);
            // effect.Play();
        }
    }
}
