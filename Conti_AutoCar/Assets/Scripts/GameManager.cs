using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private Transform centerPoint, playerCamera, player;
    [SerializeField]
    private GameObject spherePrefab;
    private GameObject sphereObj;
    [SerializeField]
    private ControllerCollider leftCollider, rightCollider;
    private bool leftGrabDown, rightGrabDown = false;
    public float currentSpeed = 2f;
    private float angle, height, radius = 0f;
    private float spawnX, spawnY, spawnZ = 0f;
    private bool ball1set = false;
    public bool OKspawn = true;
    public int score, missed = 0;
    public bool gameOver = false;
    [SerializeField]
    private GameObject scoreboardCanvas;
    [SerializeField]
    private TMP_Text scoreText;

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

        //Speed up
        if (currentSpeed > 0f)
        {
            currentSpeed -= 1 / 30.0f * Time.deltaTime;
        }
        else if(currentSpeed <= 0f)
        {
            currentSpeed = 0f;
        }
        //Gameover condition
        if (missed >= 4)
        {
            gameOver = true;
            StopCoroutine(coroutine);
        }
        if (gameOver)
        {
            scoreboardCanvas.SetActive(true);
            scoreText.text = "Score: " + score;
        }
        scoreText.text = "Score: " + score;
    }
    
    private IEnumerator RoutineSpawn()
    {
        int loopCount = 0;
        Debug.Log("spawn started");
        while (!gameOver)
        {
            WaitForSecondsRealtime wait = new(currentSpeed);
            yield return wait;

            angle = Random.Range(-70f, 70f);
            height = Random.Range(-0.15f, 0.15f);
            radius = Random.Range(-0.35f, -0.4f);
            while (CheckPosSpawn(angle, height, radius))
            {
                Debug.Log("finding new spawn pos");
                angle = Random.Range(-70f, 70f);
                height = Random.Range(-0.15f, 0.15f);
                radius = Random.Range(-0.35f, -0.4f);
                loopCount++;
                if (loopCount >= 10)
                {
                    break;
                }
            }
            if (loopCount < 10)
            {
                loopCount = 0;
                OKspawn = true;
            }
            else if (loopCount >= 10)
            {
                loopCount = 0;
                OKspawn = false;
            }

            if (!ball1set)
            {
                sphereObj = Instantiate(spherePrefab, new Vector3(spawnX, spawnY, spawnZ) + new Vector3(centerPoint.position.x, 0, centerPoint.position.z), Quaternion.identity);
                sphereObj.GetComponent<MeshRenderer>().enabled = false;
                sphereObj.GetComponent<BallSpawn>().enabled = false;
                ball1set = true;
            }
            else if (ball1set && OKspawn)
            {
                sphereObj.GetComponent<MeshRenderer>().enabled = true;
                sphereObj.GetComponent<BallSpawn>().enabled = true;
                Instantiate(spherePrefab, new Vector3(spawnX, spawnY, spawnZ) + new Vector3(centerPoint.position.x, 0, centerPoint.position.z), Quaternion.identity);
                ball1set = false;
                sphereObj = null;
            }
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

        return Physics.CheckSphere(new Vector3(spawnX, spawnY, spawnZ) + new Vector3(centerPoint.position.x, 0, centerPoint.position.z), 0.15f);
    }
    public void Restart()
    {
        currentSpeed = 2f;
        score = 0;
        missed = 0;
        gameOver = false;
        scoreboardCanvas.SetActive(false);
        coroutine = StartCoroutine(RoutineSpawn());
    }
    public void Exit()
    {
        currentSpeed = 2f;
        score = 0;
        missed = 0;
        gameOver = false;
        scoreboardCanvas.SetActive(false);
    }
}
