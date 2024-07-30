using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollider : MonoBehaviour
{
    private GameObject detectedBall;
    private bool ballDetected = false;
    public bool BallDetected { get => ballDetected; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("ball detected");
            ballDetected = true;
            detectedBall = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("ball left");
            ballDetected = false;
            detectedBall = null;
        }
    }

    public void DestroyBall()
    {
        if (detectedBall != null)
        {
            Destroy(detectedBall);
            detectedBall = null;
            ballDetected = false;
        }
    }
}
