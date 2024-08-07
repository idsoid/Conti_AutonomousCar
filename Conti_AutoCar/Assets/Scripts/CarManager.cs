using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer displayMeshRenderer;
    [SerializeField]
    private ToggleGroup themeChoice;
    private int themeNumber;
    [SerializeField]
    private List<Material> screenMaterial;
    [SerializeField]
    private GameObject gameHead, gameManager;
    private bool musicSelected = false;
    public bool MusicSelected { get => musicSelected; set => musicSelected = value; }
    [SerializeField]
    private VideoPlayer musicTracker, bgTracker;
    [SerializeField]
    private GameObject rectangleUI, textUI, fullscreenUI;

    // Start is called before the first frame update
    void Start()
    {
        displayMeshRenderer.material = screenMaterial[6];
    }

    // Update is called once per frame
    void Update()
    {
        
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
            fullscreenUI.SetActive(false);
            StartCoroutine(VideoTimestamp(vid));
        }
    }
    private IEnumerator VideoTimestamp(VideoPlayer vid)
    {
        if (musicTracker.isPlaying)
        {
            musicTracker.Pause();
        }

        vid.Prepare();
        yield return new WaitUntil(() => vid.isPrepared);
        yield return new WaitUntil(() => vid.canSetTime);

        musicTracker.Play();
        vid.Play();
        vid.time = musicTracker.time;
    }
    public void PlayMusic()
    {
        if (musicSelected && musicTracker.isPaused)
        {
            musicTracker.Play();
        }
    }
    public void PauseMusic()
    {
        if (musicSelected && musicTracker.isPlaying)
        {
            musicTracker.Pause();
        }
    }
    public void MuteBackground()
    {
        bgTracker.SetDirectAudioMute(0, true);
    }
    public void UnmuteBackground()
    {
        bgTracker.SetDirectAudioMute(0, false);
    }
}
