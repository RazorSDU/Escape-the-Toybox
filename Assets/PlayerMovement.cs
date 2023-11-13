using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of movement.
    public float upSpeed = 4f;
    public float downSpeed = 1.5f;
    public float leftSpeed = 2f;
    public float rightSpeed = 2f;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float horizontalSpeed = horizontalInput * (horizontalInput > 0 ? rightSpeed : leftSpeed);
        float verticalSpeed = verticalInput * (verticalInput > 0 ? upSpeed : downSpeed);

        Vector3 movement = new Vector3(horizontalSpeed, verticalSpeed, 0) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        animator.SetFloat("Horizontal", horizontalSpeed * 500);
        animator.SetFloat("Vertical", verticalSpeed * 500);
        animator.SetFloat("Speed", movement.sqrMagnitude * 100);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Front"))
        {
            transform.Translate(Vector3.up * upSpeed * 0.6f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collisions if needed.
    }
}