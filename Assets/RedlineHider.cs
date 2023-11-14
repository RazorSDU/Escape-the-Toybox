using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineHider : MonoBehaviour
{
    // Reference to the player GameObject
    public GameObject player;

    // Adjust this value to control the transparency range
    public float maxYDistance = 10f;

    void Update()
    {
        // Ensure that the player reference is set
        if (player != null)
        {
            // Calculate the absolute y-coordinate difference between the object and the player
            float yDistance = Mathf.Abs(transform.position.y - player.transform.position.y);

            // Calculate the transparency based on the y-coordinate difference
            float transparency = Mathf.Clamp01(1f - yDistance / maxYDistance);

            // Set the transparency of the object's material
            SetObjectTransparency(transparency);
        }
        else
        {
            Debug.LogError("Player reference not set in the inspector!");
        }
    }

    void SetObjectTransparency(float transparency)
    {
        // Ensure the object has a material with transparency support
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Get the material of the object
            Material material = renderer.material;

            // Set the alpha value of the material's color
            Color color = material.color;
            color.a = transparency;
            material.color = color;
        }
        else
        {
            Debug.LogError("Renderer component not found on the object!");
        }
    }
}
