using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Mover : MonoBehaviour
{
    public float moveSpeed = 10.0f; // Adjust the speed as needed
    public float teleportY = -135.0f;
    public float maxY = 90.0f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Move the object towards +y at a constant speed
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Check if the object has reached or passed the maxX
        if (transform.position.y >= maxY)
        {
            // Teleport the object to the specified position
            Teleport();
        }
    }

    private void Teleport()
    {
        Vector3 newPosition = new Vector3(transform.position.x, teleportY, transform.position.z);
        transform.position = newPosition;
    }
}
