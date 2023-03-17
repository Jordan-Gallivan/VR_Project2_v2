using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class ActionListener : MonoBehaviour
{
    // Initialize boolean control values
    public TractorBeam tractorBeam;
    public GameScript player;

    public XRNode leftxrNode = XRNode.LeftHand;
    private List<InputDevice> leftDevices = new List<InputDevice>();
    private InputDevice leftDevice;

    private XRNode rightxrNode = XRNode.LeftHand;
    private List<InputDevice> rightDevices = new List<InputDevice>();
    private InputDevice rightDevice;

    private bool leftGrasp;
    private bool leftTrig;
    private bool rightGrasp;
    private bool rightTrig;

    private Vector2 rightTrackPad = Vector2.zero;


    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(leftxrNode, leftDevices);
        leftDevice = leftDevices.FirstOrDefault();

        InputDevices.GetDevicesAtXRNode(rightxrNode, rightDevices);
        rightDevice = rightDevices.FirstOrDefault();
    }

    private void OnEnable()
    {
        if (!leftDevice.isValid || !rightDevice.isValid)
        {
            GetDevice();
        }
    }

    void Start()
    {
        // initialize boolean control values
        // UnityEngine.XR.Input
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftDevice.isValid || !rightDevice.isValid)
        {
            GetDevice();
        }

        // private bool leftGrasp;
        // private bool leftTrig;
        // private bool rightGrasp;
        // private bool rightTrig;

        InputFeatureUsage<bool> leftGraspUsage = CommonUsages.gripButton;
        leftDevice.TryGetFeatureValue(leftGraspUsage, out leftGrasp);
        InputFeatureUsage<bool> leftTrigUsage = CommonUsages.triggerButton;
        InputFeatureUsage<bool> rightGraspUsage = CommonUsages.gripButton;
        InputFeatureUsage<bool> rightTrigUsage = CommonUsages.triggerButton;
        InputFeatureUsage<Vector2> rightTrackPadUsage = CommonUsages.primary2DAxis;

        // left grasp => Activate Tractor Beam
        if (leftDevice.TryGetFeatureValue(leftGraspUsage, out leftGrasp) && leftGrasp)
        {
            Debug.Log("Left Grasp Works");
            tractorBeam.ActivateTractorBeam();
        }
        //else
        //{
        //    Debug.Log("Left Grasp Off");
        //    tractorBeam.DeactivateTractorBeam();
        //}

        // left trigger => Summon Object
        if (leftDevice.TryGetFeatureValue(leftTrigUsage, out leftTrig) && leftTrig)
        {
            Debug.Log("Left Trig Works");
            tractorBeam.SummonObject();
        }
        //else
        //{
        //    tractorBeam.DeactivateSummon();
        //}

        // right grasp => Rotate Object
        if (rightDevice.TryGetFeatureValue(rightGraspUsage, out rightGrasp) && rightGrasp)
        {
            Debug.Log("Right Grasp Works");
            // need to add method for rotation
        }

        // right trigger => Move Object
        if (rightDevice.TryGetFeatureValue(rightTrigUsage, out rightTrig) && rightTrig)
        {
            Debug.Log("Right Trig Works");
            // need to add method for movement
        }

        // right trackpad => move player
        if (rightDevice.TryGetFeatureValue(rightTrackPadUsage, out rightTrackPad) && rightTrackPad != Vector2.zero)
        {
            Debug.Log($"Right TrackPad Works {rightTrackPad}");
            // need to add method for moving player
        }
        player.MovePlayer(rightTrackPad);

    }

}
