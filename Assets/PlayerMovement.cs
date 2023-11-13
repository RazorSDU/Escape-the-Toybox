using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement.
    public Animator animator;

    private void Update()
    {
        // Get input values for horizontal and vertical movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position based on input and speed.
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

        // Apply the movement to the object's position.
        transform.Translate(movement);

        animator.SetFloat("Horizontal", movement.x * 100);
        animator.SetFloat("Vertical", movement.y * 100);
        animator.SetFloat("Speed", movement.sqrMagnitude * 100);

        //// Check if the vertical movement is greater than or equal to 1 or smaller than or equal to -1.
        //if (movement.y * 100 >= 1)
        //{
        //    // Set the Speed parameter in the "Movement" Blend Tree to 1.
        //    animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        //}
        //else if (movement.y * 100 <= -1)
        //{
        //    // Set the Speed parameter in the "Movement" Blend Tree to 0.5.
        //    animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        //}

        //Debug.Log($"X: {movement.x}, Y: {movement.y}, SP: {movement.sqrMagnitude}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collisions if needed.
    }
}