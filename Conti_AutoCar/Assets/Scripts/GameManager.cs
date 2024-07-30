using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using TMPro;
>>>>>>> d41319eb913037c54f1c5c5923971a8707d13dce

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private Coroutine coroutine;
    [SerializeField]
    private Transform centerPoint, playerCamera;
    [SerializeField]
    private GameObject spherePrefab;
    [SerializeField]
    private ControllerCollider leftCollider, rightCollider;
    private bool leftGrabDown, rightGrabDown = false;
    public float currentSpeed = 2f;
    private float angle, height, radius = 0f;
    private float spawnX, spawnY, spawnZ = 0f;
    public int score, missed = 0;
    private bool gameOver = false;
    [SerializeField]
    private GameObject scoreboardCanvas;
<<<<<<< HEAD
=======
    [SerializeField]
    private TextMeshPro scoreText;
>>>>>>> d41319eb913037c54f1c5c5923971a8707d13dce

    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(RoutineSpawn());
    }
    
    // Update is called once per frame
    void Update()
    {
        //Left hand grab check
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) >= 0.95f && !leftGrabDown)
        {
            Debug.Log("lhand grab down");
            leftGrabDown = true;
            if (leftCollider.BallDetected)
            {
                leftCollider.DestroyBall();
                score++;
            }
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
            if (rightCollider.BallDetected)
            {
                rightCollider.DestroyBall();
                score++;
            }
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) <= 0.5f && rightGrabDown)
        {
            Debug.Log("rhand grab up");
            rightGrabDown = false;
        }

        //Position check and offset
        if (Vector2.Distance(new Vector2(playerCamera.position.x, playerCamera.position.z), new Vector2(centerPoint.position.x, centerPoint.position.z)) >= 0.2f)
        {
            Vector3 cameraOffset = playerCamera.position - centerPoint.position;
            transform.position = new Vector3(transform.position.x - cameraOffset.x, transform.position.y, transform.position.z - cameraOffset.z);
        }

        if (currentSpeed > 0f)
        {
            currentSpeed -= 1 / 30.0f * Time.deltaTime;
        }
        else if(currentSpeed <= 0f)
        {
            currentSpeed = 0f;
        }
        //Gameover condition
        if (missed >= 5)
        {
            gameOver = true;
            StopCoroutine(coroutine);
        }
        if (gameOver)
        {
            scoreboardCanvas.SetActive(true);
<<<<<<< HEAD
=======
            scoreText.text = "Score: " + score;
>>>>>>> d41319eb913037c54f1c5c5923971a8707d13dce
        }
    }
    
    private IEnumerator RoutineSpawn()
    {
        while (!gameOver)
        {
            WaitForSecondsRealtime wait = new(currentSpeed);
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
    public void Restart()
    {
        coroutine = StartCoroutine(RoutineSpawn());
        currentSpeed = 2f;
        score = missed = 0;
        gameOver = false;
        scoreboardCanvas.SetActive(false);
    }
    public void Home()
    {
        scoreboardCanvas.SetActive(false);
    }
}
