using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CarManager : MonoBehaviour
{
    //Preferences
    [SerializeField]
    private MeshRenderer displayMeshRenderer;
    [SerializeField]
    private ToggleGroup themeChoice;
    public int themeNumber = 1;
    [SerializeField]
    private List<Material> screenMaterial;
    //Game
    [SerializeField]
    private GameObject gameHead, gameManager;
    //Music
    private bool musicSelected = false;
    public bool MusicSelected { get => musicSelected; set => musicSelected = value; }
    [SerializeField]
    private VideoPlayer musicTracker, bgTracker, leftDisplay, rightDisplay;
    [SerializeField]
    private GameObject rectangleUI, textUI, fullscreenUI, pauseplayButton;
    private bool musicPaused = false;
    //Face Analysis
    [SerializeField]
    private GameObject exitButton;
    [SerializeField]
    private List<SpriteRenderer> faceImages;
    private bool scanning, increasingAlpha = false;
    private float faceTimer = 0f;
    private int dir = 1;
    public int step = 0;
    //DigitalCompanion
    [SerializeField]
    private Animator dcAnimator;

    // Start is called before the first frame update
    void Start()
    {
        displayMeshRenderer.material = screenMaterial[6];
    }

    // Update is called once per frame
    void Update()
    {
        //Face Scan animation
        if (scanning)
        {
            switch (step)
            {
                case 0: //Instructions
                    //Fade in
                    if (faceImages[0].color.a < 1f && increasingAlpha)
                    {
                        Color color = faceImages[0].color;
                        color.a += Time.deltaTime;
                        faceImages[0].color = color;
                    }
                    else if (faceImages[0].color.a >= 1f)
                    {
                        increasingAlpha = false;
                        faceTimer += Time.deltaTime;
                    }

                    //Fade out
                    if (faceTimer >= 3.0f && faceImages[0].color.a > 0f && !increasingAlpha)
                    {
                        Color color = faceImages[0].color;
                        color.a -= Time.deltaTime;
                        faceImages[0].color = color;
                    }
                    else if (faceImages[0].color.a <= 0f && !increasingAlpha)
                    {
                        faceTimer = 0f;
                        increasingAlpha = true;
                        faceImages[0].gameObject.SetActive(false);
                        faceImages[1].gameObject.SetActive(true);
                        step++;
                    }
                    break;
                case 1: //Face
                    //Fade in
                    if (faceImages[1].material.color.a < 1f)
                    {
                        Color color = faceImages[1].material.color;
                        color.a += Time.deltaTime * 0.75f;
                        faceImages[1].material.color = color;
                    }
                    else if (faceImages[1].material.color.a >= 1f)
                    {
                        faceImages[2].gameObject.SetActive(true);
                        faceImages[3].gameObject.SetActive(true);
                        step++;
                    }
                    break;
                case 2: //Scanning
                    //Fade in
                    if (faceImages[2].color.a < 1f && increasingAlpha)
                    {
                        Color color = faceImages[2].color;
                        color.a += Time.deltaTime * 1.5f;
                        faceImages[2].color = color;
                        color = faceImages[3].color;
                        color.a += Time.deltaTime * 1.5f;
                        faceImages[3].color = color;
                    }
                    else if (faceImages[2].color.a >= 1f)
                    {
                        increasingAlpha = false;
                    }

                    if (!increasingAlpha)
                    {
                        faceTimer += Time.deltaTime;
                        faceImages[2].transform.localPosition += new Vector3(0, Time.deltaTime * 0.1f * dir, 0);
                        if (faceImages[2].transform.localPosition.y >= 0.1 || faceImages[2].transform.localPosition.y <= -0.09)
                        {
                            dir *= -1;
                        }

                        if (faceTimer >= 3.0f && faceImages[3].color.a > 0f)
                        {
                            Color color = faceImages[3].color;
                            color.a -= Time.deltaTime;
                            faceImages[3].color = color;
                            color = faceImages[1].material.color;
                            color.r -= Time.deltaTime;
                            color.g -= Time.deltaTime * 0.5f;
                            faceImages[1].material.color = color;
                        }
                        else if (faceImages[3].color.a <= 0f)
                        {
                            faceImages[3].gameObject.SetActive(false);
                            faceImages[4].gameObject.SetActive(true);
                            faceTimer = 0f;
                            increasingAlpha = true;
                            step++;
                        }
                    }
                    break;
                case 3:
                    faceImages[2].transform.localPosition += new Vector3(0, Time.deltaTime * 0.1f * dir, 0);
                    if (faceImages[2].transform.localPosition.y >= 0.1 || faceImages[2].transform.localPosition.y <= -0.09)
                    {
                        dir *= -1;
                    }

                    if (faceImages[4].color.a < 1f && increasingAlpha)
                    {
                        Color color = faceImages[4].color;
                        color.a += Time.deltaTime * 1.5f;
                        faceImages[4].color = color;
                    }
                    else if (faceImages[4].color.a >= 1f)
                    {
                        increasingAlpha = false;
                    }

                    if (!increasingAlpha)
                    {
                        faceTimer += Time.deltaTime;
                        if (faceTimer >= 3.0f && faceImages[4].color.a > 0f)
                        {
                            Color color = faceImages[1].material.color;
                            color.a -= Time.deltaTime;
                            faceImages[1].material.color = color;
                            color = faceImages[2].color;
                            color.a -= Time.deltaTime;
                            faceImages[2].color = color;
                            color = faceImages[4].color;
                            color.a -= Time.deltaTime;
                            faceImages[4].color = color;
                        }
                        else if (faceImages[4].color.a <= 0f)
                        {
                            faceImages[1].gameObject.SetActive(false);
                            faceImages[2].gameObject.SetActive(false);
                            faceImages[4].gameObject.SetActive(false);
                            exitButton.SetActive(true);
                            displayMeshRenderer.material = screenMaterial[5];
                            step++;
                            //Color color = displayMeshRenderer.material.color;
                            //color.a = 0;
                            //displayMeshRenderer.material.color = color;
                        }

                        //if (displayMeshRenderer.material.color.a < 1f)
                        //{
                        //    Color color = displayMeshRenderer.material.color;
                        //    color.a += Time.deltaTime * 1.5f;
                        //    displayMeshRenderer.material.color = color;
                        //}
                        //else if (displayMeshRenderer.material.color.a >= 1f)
                        //{
                        //    step++;
                        //}
                    }
                    break;
                case 4:
                    scanning = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void ToggleGame()
    {
        if (!gameManager.activeSelf)
        {
            gameHead.SetActive(!gameHead.activeSelf);
        }
    }

    //Preference saving
    public void SaveTheme()
    {
        foreach (var toggle in themeChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                switch (toggle.name)
                {
                    case "sand":
                        themeNumber = 0;
                        break;
                    case "futuristic":
                        themeNumber = 1;
                        break;
                    case "honeycomb":
                        themeNumber = 2;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }
    public void SetTheme()
    {
        displayMeshRenderer.material = screenMaterial[themeNumber];
    }

    //Music stuff
    public void MusicCheck(VideoPlayer vid)
    {
        if (musicSelected)
        {
            rectangleUI.SetActive(false);
            textUI.SetActive(false);
            fullscreenUI.SetActive(true);
            pauseplayButton.SetActive(true);
            if (dcAnimator.GetInteger("State") != 1)
            {
                dcAnimator.SetInteger("State", 1);
            }
            StartCoroutine(VideoTimestamp(vid));
        }
    }
    private IEnumerator VideoTimestamp(VideoPlayer vid)
    {
        if (!musicPaused)
        {
            musicTracker.Pause();
        }

        vid.Prepare();
        yield return new WaitUntil(() => vid.isPrepared);
        yield return new WaitUntil(() => vid.canSetTime);

        if (!musicPaused)
        {
            musicTracker.Play();
        }
        vid.SetDirectAudioMute(0, true);
        vid.Play();
        vid.time = musicTracker.time;
        if (musicPaused)
        {
            vid.Pause();
        }
    }
    public void MusicFullscreenMode()
    {
        leftDisplay.SetDirectAudioMute(0, true);
        rightDisplay.SetDirectAudioMute(0, true);
    }
    public void ExitFullscreeen()
    {
        leftDisplay.SetDirectAudioMute(0, false);
        rightDisplay.SetDirectAudioMute(0, false);
    }
    public void MuteBackground()
    {
        bgTracker.SetDirectAudioMute(0, true);
    }
    public void UnmuteBackground()
    {
        bgTracker.SetDirectAudioMute(0, false);
    }

    public void PausePlayVideo(VideoPlayer vid)
    {
        if (vid.isPlaying)
        {
            vid.Pause();
            musicPaused = true;
        }
        else if (vid.isPaused)
        {
            vid.Play();
            musicPaused = false;
        }
    }

    public void FaceAnalysis()
    {
        Color color = faceImages[1].material.color;
        color.r = 1;
        color.g = 1;
        faceImages[1].material.color = color;
        faceImages[2].transform.localPosition = new Vector3(-0.40019992f, 0.0999999791f, -0.0053f);

        scanning = true;
        faceImages[0].gameObject.SetActive(true);
        increasingAlpha = true;
        step = 0;
    }

    public void CompanionState(int state)
    {
        if (dcAnimator.GetInteger("State") != state)
        {
            dcAnimator.SetInteger("State", state);
        }
    }
}
