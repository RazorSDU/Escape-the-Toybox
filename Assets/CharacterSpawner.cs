using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public UISpawner uiManager; // Reference to the UIManager script
    public float spawnDelay = 14.5f; // Adjust this value to set the delay in seconds
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Disable the character initially
        if (characterPrefab != null)
        {
            characterPrefab.SetActive(false);
        }
        else
        {
            Debug.LogError("CharacterPrefab is not assigned in the Inspector.");
        }
        
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            float currentTime = (float)videoPlayer.time;
            if (currentTime > spawnDelay)
            {
                // Check if the characterPrefab is not null before spawning
                if (characterPrefab != null)
                {
                    // Enable the character
                    characterPrefab.SetActive(true);
                }
                else
                {
                    Debug.LogError("CharacterPrefab is not assigned in the Inspector.");
                }

                // Call the method in the UIManager to enable the UI element
                if (uiManager != null)
                {
                    uiManager.EnableUIElement();
                }
                else
                {
                    Debug.LogError("UIManager is not assigned in the Inspector.");
                }
            }
        }
    }
}
