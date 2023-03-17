using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameScript : MonoBehaviour
{
    public float walkingSpeed = .5f;
    public float runningSpeed = 10f;
    public Rigidbody rb;
    public int maxDistance = 2;
    public float minDistance = 1;

    public GameObject player;
    public float playerHeight = 1.5f;

    public float velocityConstant = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        rayDown();
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.T))
        {
            //print("Forward");
            rb.velocity = transform.forward * walkingSpeed;
        }

        else if (Input.GetKey(KeyCode.G))
        {
            //print("Backwards");
            rb.velocity = -transform.forward * walkingSpeed;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            //print("Left");
            rb.velocity = -transform.right * walkingSpeed;
            
        }
        else if (Input.GetKey(KeyCode.H))
        {
            //print("Right");
            rb.velocity = transform.right * walkingSpeed;
        }
        else
        {
            rb.velocity = transform.forward * 0;
        }
    }
    public void goForward()
    {
        rb.velocity = transform.forward * walkingSpeed;
    }

    public void goLeft()
    {
        rb.velocity = -transform.right * walkingSpeed;
    }

    public void goRight()
    {
        rb.velocity = transform.right * walkingSpeed;
    }

    public void goBack()
    {
        rb.velocity = -transform.forward * walkingSpeed;
    }

    public void MovePlayer(Vector2 trackPad)
    {
        // z = forward (vector2.y)
        // x = strafe (vector2.x)
        
        rb.AddForce(trackPad.x * velocityConstant, 0f , trackPad.y * velocityConstant, 
            ForceMode.VelocityChange);
    }

    public void rayDown()
    {
        //////////////  cleaned up code //////////////////
        RaycastHit hit;
        Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.down), out hit, 100f);
        player.transform.position = new Vector3(player.transform.position.x, hit.point.y + playerHeight, player.transform.position.z);

        /////////////   old code  ///////////////
        // Vector3 m = new Vector3(0, 2f, 0);  // what is this?
        // Physics.Raycast(new Vector3(transform.position.x, transform.position.y +1, transform.position.z), transform.TransformDirection(Vector3.down), out hit, 100f);
        // GameObject.Find("Player").transform.position.y = hit.point.y);
        // GameObject p = GameObject.Find("Player");
        // Vector3 temp = new Vector3(0.0f,hit.point.y,0);
        // transform.position = hit.point;
        // Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity);





    }
}
