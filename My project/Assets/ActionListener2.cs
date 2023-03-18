using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR;

/*
 * To-Do
 *  o add moveableItem to everything we want to move
 *  o add summonable tag to everything we want to select
 *  o refine rotation
 *  o refine movement
 *  o drop down selection on unity for movement patterns
 */

public class ActionListener2 : MonoBehaviour
{
    // TractorBeam and Player Objects
    public TractorBeam tractorBeam;
    public GameScript player;
    
    // inputs
    [SerializeField] private InputActionReference leftTrig;
    [SerializeField] private InputActionReference rightTrig;
    [SerializeField] private InputActionReference leftGrasp;
    [SerializeField] private InputActionReference rightGrasp;
    [SerializeField] private InputActionReference trackPad;
    [SerializeField] private InputActionReference rightTrackPad;
    [SerializeField] private InputActionReference leftControllerPos;
    [SerializeField] private InputActionReference leftControllerRotation;

    // Boolean Control values
    private bool itemIsMoving;
    private bool itemIsRotating;
    
    // Do we need???
    private Vector3 origControllerPos = Vector3.zero;
    private Vector3 updatedControllerPos = Vector3.zero;
    private Quaternion origControllerRot = Quaternion.identity;
    private Quaternion updatedControllerRot = Quaternion.identity;

    void Start()
    {
        itemIsMoving = false;
        itemIsRotating = false;
        origControllerPos = leftControllerPos.action.ReadValue<Vector3>();
        updatedControllerPos = origControllerPos;
        origControllerRot = leftControllerRotation.action.ReadValue<Quaternion>();
        updatedControllerRot = origControllerRot;
    }

    // Update is called once per frame
    void Update()
    {
        updatedControllerPos = leftControllerPos.action.ReadValue<Vector3>();
        updatedControllerRot = leftControllerRotation.action.ReadValue<Quaternion>();

        /////////////////////////////////////////
        /////   Detect controller inputs    /////
        /////////////////////////////////////////

        // left Grasp => Activate Tractor Beam
        if (leftGrasp.action.WasPerformedThisFrame())
        {
            Debug.Log("Left Grasp Works");
            tractorBeam.ActivateTractorBeam();
        }
        if (leftGrasp.action.WasReleasedThisFrame())
        {
            Debug.Log("Left Grasp Deselected");
            tractorBeam.DeactivateTractorBeam();
        }

        // left Trig => Select NearestItem
        if (leftTrig.action.WasPerformedThisFrame())
        {
            Debug.Log("Left Trig Works");
            tractorBeam.SelectNearestItem();
        }
        if (leftTrig.action.WasReleasedThisFrame())
        {
            Debug.Log("Left Trig deselected");
            tractorBeam.DeSelectNearestItem();
        }
        
        // right grasp => Move and Rotate Object
        if (rightGrasp.action.WasPerformedThisFrame())
        {
            Debug.Log("Right Grasp Works");
            itemIsMoving = true;
            itemIsRotating = true;
        }
        if (rightGrasp.action.WasReleasedThisFrame())
        {
            Debug.Log("Right Grasp Deselected");
            tractorBeam.EndMovement();
            tractorBeam.EndRotation();
            itemIsMoving = false;
            itemIsRotating = false;
        }
        
        // right trigger => Summon Object
        if (rightTrig.action.WasPerformedThisFrame())
        {
            Debug.Log("Right Trig Works");
            tractorBeam.SummonObject();
        }
        if (rightTrig.action.WasReleasedThisFrame())
        {
            Debug.Log("Right Trig deselected");
            tractorBeam.DeactivateSummon();
        }
        
        /////////////////////////////////////////
        /////   Update Player and Item      /////
        /////////////////////////////////////////

        // move and rotate the player according to trackpad input
        player.MovePlayer(trackPad.action.ReadValue<Vector2>());
        player.RotatePlayer(rightTrackPad.action.ReadValue<Vector2>());
        
        // move and rotate selected item
        if (itemIsMoving && itemIsRotating)
        {
            tractorBeam.MoveItem(updatedControllerPos - origControllerPos);
            tractorBeam.RotateItem(updatedControllerRot.eulerAngles - origControllerRot.eulerAngles);
        }

        origControllerPos = updatedControllerPos;
        origControllerRot = updatedControllerRot;
    }

}
