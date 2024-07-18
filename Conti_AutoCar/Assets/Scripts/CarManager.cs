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
    }
}
