using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform centerPoint;
    [SerializeField]
    private GameObject spherePrefab;
    private GameObject sphereObj;

    // Start is called before the first frame update
    void Start()
    {
        // Convert angle to radians
        float angleInRadians = Random.Range(0f, 90f) * Mathf.Deg2Rad;

        // Calculate position
        float x = Mathf.Cos(angleInRadians) * 0.75f;
        float z = Mathf.Sin(angleInRadians) * 0.75f;
        Vector3 spawnPosition = new Vector3(x, 0, z) + centerPoint.forward;

        // Instantiate the object
        sphereObj = Instantiate(spherePrefab, spawnPosition, centerPoint.rotation);
        StartCoroutine(Stuff());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Stuff()
    {
        WaitForSecondsRealtime wait = new(1f);

        while (true)
        {
            yield return wait;
            SpawnObject(Random.Range(-45f, 45f), Random.Range(-0.25f, 0.25f), Random.Range(0.5f, 0.75f));
        }
    }

    void SpawnObject(float angle, float height, float radius)
    {
        // Convert angle to radians
        float angleInRadians = -(angle + centerPoint.eulerAngles.y - 90f) * Mathf.Deg2Rad;

        // Calculate position
        float x = Mathf.Cos(angleInRadians) * radius;
        float z = Mathf.Sin(angleInRadians) * radius;
        float y = centerPoint.position.y + height;
        sphereObj.transform.position = new Vector3(x, y, z) + centerPoint.position;
    }
}
