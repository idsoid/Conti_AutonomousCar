using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform centerPoint, playerCam;
    [SerializeField]
    private GameObject spherePrefab;
    private bool leftGrabDown, rightGrabDown = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoutineSpawn());
    }
    
    // Update is called once per frame
    void Update()
    {
        //Left hand grab check
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) >= 0.95f && !leftGrabDown)
        {
            Debug.Log("lhand grab down");
            leftGrabDown = true;
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) <= 0.5f && leftGrabDown)
        {
            Debug.Log("lhand grab up");
            leftGrabDown = false;
        }
        //Right hand grab check
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) >= 0.95f && !rightGrabDown)
        {
            Debug.Log("rhand grab down");
            rightGrabDown = true;
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) <= 0.5f && rightGrabDown)
        {
            Debug.Log("rhand grab up");
            rightGrabDown = false;
        }
    }

    private IEnumerator RoutineSpawn()
    {
        WaitForSecondsRealtime wait = new(2f);

        while (true)
        {
            yield return wait;

            

            SpawnObject(Random.Range(-90f, 90f), Random.Range(-0.3f, 0.3f), Random.Range(0.5f, 0.6f));
        }
    }
    private void SpawnObject(float angle, float height, float radius)
    {
        // Convert angle to radians
        float angleInRadians = -(angle + centerPoint.eulerAngles.y - 90f) * Mathf.Deg2Rad;

        // Calculate position
        float x = Mathf.Cos(angleInRadians) * radius;
        float z = Mathf.Sin(angleInRadians) * radius;
        float y = playerCam.position.y + height;
        Instantiate(spherePrefab, new Vector3(x, y, z) + centerPoint.position, Quaternion.identity);
    }
    private void CheckSpawn()
    {
        float angle = Random.Range(-90f, 90f);
        float height = Random.Range(-0.3f, 0.3f);
        float radius = Random.Range(0.5f, 0.6f);

        Physics.CheckSphere(new Vector3(angle, height, radius) + centerPoint.position, 0.125f);
    }
}
