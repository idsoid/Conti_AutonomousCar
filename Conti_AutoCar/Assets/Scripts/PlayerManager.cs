using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> carPoints; 
    [SerializeField]
    private Transform playerCamera, playerMaxHeight, playerMinHeight, playerMaxLength, playerMinLength;
    public bool playerSet = false;
    private int adjustCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Initial spawning of user
        if (OVRPlugin.userPresent && OVRManager.isHmdPresent && !playerSet)
        {
            Debug.Log("Headset Mounted");
            transform.position = new Vector3(carPoints[0].position.x, transform.position.y, carPoints[0].position.z);
            playerSet = true;
        }
        else if (!OVRPlugin.userPresent && !OVRManager.isHmdPresent && playerSet)
        {
            playerSet = false;
        }

        //Seat adjustment
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).y >= 0.9f && playerCamera.position.y < playerMaxHeight.position.y)
        {
            transform.position += new Vector3(0, Time.deltaTime * 0.25f, 0);
        }
        else if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).y <= -0.9f && playerCamera.position.y > playerMinHeight.position.y)
        {
            transform.position -= new Vector3(0, Time.deltaTime * 0.25f, 0);
        }
        Debug.Log("left thumbstick: " + OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch));

        //Reset Orientation and Position
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            adjustCount = 0;
            Vector3 cameraOffset = playerCamera.position - carPoints[0].position;
            transform.position = new Vector3(transform.position.x - cameraOffset.x, transform.position.y, transform.position.z - cameraOffset.z);
        }
    }

    public void SeatAdjustment(string direction)
    {
        switch (direction)
        {
            case "FORWARD":
                if (adjustCount < 3)
                {
                    adjustCount++;
                    transform.position -= new Vector3(0, 0, 0.02f);
                }
                break;
            case "BACKWARD":
                if (adjustCount > -3)
                {
                    adjustCount--;
                    transform.position += new Vector3(0, 0, 0.02f);
                }
                break;
            case "UP":
                if (playerCamera.position.y < playerMaxHeight.position.y)
                {
                    transform.position += new Vector3(0, 0.02f, 0);
                }
                break;
            case "DOWN":
                if (playerCamera.position.y > playerMinHeight.position.y)
                {
                    transform.position -= new Vector3(0, 0.02f, 0);
                }
                break;
            default:
                break;
        }
    }
}
