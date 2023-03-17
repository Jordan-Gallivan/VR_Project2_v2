using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MovableItem : MonoBehaviour
{
    // TractorBeam and identity variables
    public bool itemIsSelected = false;
    public bool moveItem = false;
    [SerializeField] private GameObject player;
    private GameObject item;
    
    // speed coefficients 
    private float xSpeed = 0.002f;
    private float ySpeed = 0.002f;
    private float zSpeed = 0.002f;
    private Vector3 speed = Vector3.zero;
    
    // time variables
    private float startTime;
    public float duration = 16f;
    
    // Movement Patterns
    enum Movements
    {
        ForwardAndBackward,
        LeftAndRight,
        TenThirtyToFourThirty,
        OneThirtyToSevenThirty,
        UpAndDown,
        TowardsUser,
        AwayFromUser
    };
    [SerializeField] private Movements movementPattern = new Movements();

    private float distToUser;
    // update to smaller distance interactables should be delayed
    // until user approaches within a certain distance
    public float distToBeginInteraction = Mathf.Infinity;
    private float towardsOrAway = 1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        item = this.gameObject; // initialize item as the item script attached to
        
        // initialize time
        startTime = Time.time;
        
        // determine which movement pattern user selected
        switch (movementPattern)
        {
            case Movements.ForwardAndBackward:
                speed = new Vector3(0f, 0f, zSpeed);
                break;
            case Movements.LeftAndRight:
                speed = new Vector3(xSpeed, 0f, 0f);
                break;
            case Movements.TenThirtyToFourThirty:
                speed = new Vector3(-xSpeed, 0f, zSpeed);
                break;
            case Movements.OneThirtyToSevenThirty:
                speed = new Vector3(xSpeed, 0f, zSpeed);
                break;
            case Movements.UpAndDown:
                speed = new Vector3(0f, ySpeed, 0f);
                break;
            case Movements.AwayFromUser:
                towardsOrAway = -1f;
                break;
            default:
                speed = Vector3.zero;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // update distance from user to item
        distToUser = Vector3.Distance(player.transform.position, item.transform.position);
        
        // verify item is not selected by Tractor Beam, item is movable, and within required distance to user
        if (itemIsSelected || !moveItem || (!(distToUser < distToBeginInteraction))) return;
        
        // Movement towards or away from user
        if (movementPattern is Movements.TowardsUser or Movements.AwayFromUser)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, 
                player.transform.position, towardsOrAway * 2 * Time.deltaTime);
        } 
        // all other movement patterns
        else
        {
            item.transform.Translate(speed);    // move item per movement pattern
            // reverse items course after time == duration
            if (!(Time.time - startTime > duration)) return;
            speed = new Vector3(-speed.x, -speed.y, -speed.z);  // reverse direction of movement
            startTime = Time.time;  // reset start time
        }
    }
}
