using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> carPoints;
    private int currPoint = 0;
    [SerializeField]
    private Transform playerCamera, playerMaxHeight, playerMinHeight;
    public bool playerSet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Initial spawning of user
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(carPoints[0].position.x, carPoints[0].position.z)) >= 0.1f && !playerSet)
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
            Debug.Log("x pressed");
            transform.position = new Vector3(carPoints[0].position.x, transform.position.y, carPoints[0].position.z);
            transform.localEulerAngles = Vector3.zero;
            playerCamera.localEulerAngles = Vector3.zero;
        }
    }
}
