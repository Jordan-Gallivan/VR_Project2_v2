using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CowboyScript : MonoBehaviour
{
    private GameObject tgt;
    private float startTime;
    private float durration = 6f;
    public float xSpeed = 0;
    public float ySpeed = 0;
    public float zSpeed = 0;
    public Vector3 speed;

    public float minDist = 10;
    public float dist;
    public GameObject playerObj;

    public bool playerInRange = false;
    public float lastAttackTime = 0f;

    public Rigidbody bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.startTime = Time.time;
        this.tgt = this.gameObject;
        this.speed = new Vector3(xSpeed, ySpeed, zSpeed);
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.tgt.transform.Translate(this.speed);
        if (Time.time - startTime > durration)
        {
            xSpeed *= -1;
            ySpeed *= -1;
            zSpeed *= -1;
            startTime = Time.time;
            this.speed = new Vector3(xSpeed, ySpeed, zSpeed);
        }
        distanceCall();
        if (playerInRange == true)
        {
            transform.rotation = Quaternion.LookRotation(playerObj.transform.position - transform.position, transform.up);
            if (Time.time - lastAttackTime >= 1f)
            {
                
                shoot();
                lastAttackTime = Time.time;
            }
            
            
        }
    }

    void distanceCall()
    {
        dist = Vector3.Distance(playerObj.transform.position, transform.position);
        if (dist < minDist)
        {
            playerInRange = true;
        }
        else
        {

            playerInRange = false;
        }
        
    }
    void shoot()
    {
        var projectile = Instantiate(bulletPrefab, transform.position, transform.rotation);
        projectile.velocity = transform.forward * 50;
        //yield return new WaitForSeconds(5);
        //Destroy(bulletPrefab);
    }

   
}
