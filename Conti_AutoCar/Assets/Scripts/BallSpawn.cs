using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    private float decayTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        decayTimer += Time.deltaTime;
        if (decayTimer >= 5.0f)
        {
            GameManager.Instance.missed++;
            Destroy(gameObject);
        }
    }
}
