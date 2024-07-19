using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> carPoints;
    [SerializeField]
    private Transform playerCamera;
    private bool playerSet = false;
    private float playerHeight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRPlugin.userPresent && !playerSet)
        {
            Debug.Log("Headset Mounted");
            playerCamera.transform.position = new Vector3(carPoints[0].position.x, 0, carPoints[0].position.z);
            playerSet = true;
        }
        else if (!OVRPlugin.userPresent && playerSet)
        {
            playerSet = false;
        }

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).y >= 0.9f && playerHeight <= 0.25f)
        {
            playerCamera.position += new Vector3(0, Time.deltaTime * 0.25f, 0);
            playerHeight += Time.deltaTime * 0.5f;
        }
        else if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).y <= -0.9f && playerHeight >= -0.5f)
        {
            playerCamera.position -= new Vector3(0, Time.deltaTime * 0.25f, 0);
            playerHeight -= Time.deltaTime * 0.25f;
        }
        Debug.Log("left thumbstick: " + OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch));
    }
}
