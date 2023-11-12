using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool isPaused = false;

    void Start()
    {
        // Ensure the VideoPlayer component is assigned
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
    }

    void Update()
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
    public void TogglePlayPause()
    {
        if (isPaused)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }
        isPaused = !isPaused;
    }
    public void SkipForward()
    {
        videoPlayer.time += 5f;
    }

    public void SkipBackward()
    {
        videoPlayer.time -= 5f;
    }
}