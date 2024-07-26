using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform centerPoint, player, playerCamera;
    [SerializeField]
    private GameObject spherePrefab;
    private bool leftGrabDown, rightGrabDown = false;
    private float angle, height, radius = 0f;
    private float spawnX, spawnY, spawnZ = 0f;

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

        if (Vector2.Distance(new Vector2(playerCamera.position.x, playerCamera.position.z), new Vector2(centerPoint.position.x, centerPoint.position.z)) >= 0.2f)
        {
            Vector3 cameraOffset = playerCamera.position - centerPoint.position;
            transform.position = new Vector3(transform.position.x - cameraOffset.x, transform.position.y, transform.position.z - cameraOffset.z);
        }
    }

    private IEnumerator RoutineSpawn()
    {
        WaitForSecondsRealtime wait = new(0.5f);

        while (true)
        {
            yield return wait;
            
            angle = Random.Range(-90f, 90f);
            height = Random.Range(-0.3f, 0.3f);
            radius = Random.Range(0.5f, 0.6f);
            while (CheckPosSpawn(angle, height, radius))
            {
                Debug.Log("finding new spawn pos");
                angle = Random.Range(-90f, 90f);
                height = Random.Range(-0.3f, 0.3f);
                radius = Random.Range(0.5f, 0.6f);
            }

            Instantiate(spherePrefab, new Vector3(spawnX, spawnY, spawnZ) + centerPoint.position, Quaternion.identity);
        }
    }
    private bool CheckPosSpawn(float angleCheck, float heightCheck, float radiusCheck)
    {
        // Convert angle to radians
        float angleInRadians = -(angleCheck + centerPoint.eulerAngles.y - 90f) * Mathf.Deg2Rad;
        // Calculate position
        spawnX = Mathf.Cos(angleInRadians) * radiusCheck;
        spawnZ = Mathf.Sin(angleInRadians) * radiusCheck;
        spawnY = playerCamera.position.y + heightCheck;

        return Physics.CheckSphere(new Vector3(spawnX, spawnY, spawnZ) + centerPoint.position, 0.1f);
    }
}
