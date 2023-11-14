using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public UISpawner uiManager; // Reference to the UIManager script
    public float spawnDelay = 14.5f; // Adjust this value to set the delay in seconds

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

        // Example: Enable the character and UI element after 3 seconds
        Invoke("EnableCharacterAndUI", spawnDelay);
    }

    void EnableCharacterAndUI()
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
