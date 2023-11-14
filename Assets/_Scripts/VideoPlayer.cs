using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool isPaused = false;
    private bool adminMode = false;
    public Renderer adminMap;
    public Button AdminBeatChecker;
    public TextMeshProUGUI AdminTimer;

    void Start()
    {
        // Ensure the VideoPlayer component is assigned
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        adminMap.enabled = !adminMap.enabled;
        AdminTimer.enabled = !AdminTimer.enabled;
        AdminBeatChecker.gameObject.SetActive(!AdminBeatChecker.gameObject.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Input.GetKeyDown(KeyCode.O))
        {
            ToggleAdminMode();
        }
        if (adminMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TogglePlayPause();
            }
            // Check for input to skip forward
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SkipForward();
            }

            // Check for input to skip backward
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SkipBackward();
            }
        }

    }

    private void ToggleAdminMode()
    {
        adminMode = !adminMode;
        adminMap.enabled = !adminMap.enabled;
        AdminTimer.enabled = !AdminTimer.enabled;
        AdminBeatChecker.gameObject.SetActive(!AdminBeatChecker.gameObject.activeSelf);
    }

    private void TogglePlayPause()
    {
        if (isPaused)
        {
            videoPlayer.Play();
        } else
        {
            videoPlayer.Pause();
        }
        isPaused = !isPaused;
    }

    private void SkipForward()
    {
        videoPlayer.time += 5f;
    }

    private void SkipBackward()
    {
        videoPlayer.time -= 5f;
    }
}
