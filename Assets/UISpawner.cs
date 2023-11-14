using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour
{
    public GameObject uiElement;

    void Start()
    {
        // Disable the UI element initially
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
        else
        {
            Debug.LogError("UIElement is not assigned in the Inspector.");
        }

        // Example: Enable the UI element after 3 seconds
        Invoke("EnableUIElement", 15f);
    }

    public void EnableUIElement()
    {
        // Check if the UIElement is not null before enabling
        if (uiElement != null)
        {
            // Enable the UI element
            uiElement.SetActive(true);
        }
        else
        {
            Debug.LogError("UIElement is not assigned in the Inspector.");
        }
    }
}

