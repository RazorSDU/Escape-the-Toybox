using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement.
    public float upSpeed = 4f;
    public float downSpeed = 1.5f;
    public float leftSpeed = 2f;
    public float rightSpeed = 2f;
    public float knockBackForce = 5f; //Force of the knockback.
    public float knockbackDuration = 0.5f; // Duration of the knockback.
    public Animator animator;

    //void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    private void Update()
    {
        // Get input values for horizontal and vertical movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float horizontalSpeed = horizontalInput * (horizontalInput > 0 ? rightSpeed : leftSpeed);
        float verticalSpeed = verticalInput * (verticalInput > 0 ? upSpeed : downSpeed);

        // Calculate the new position based on input and speed.
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

        // Apply the movement to the object's position.
        transform.Translate(movement);

        animator.SetFloat("Horizontal", movement.x * 500);
        animator.SetFloat("Vertical", movement.y * 500);
        animator.SetFloat("Speed", movement.sqrMagnitude * 100);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Front"))
        {
            transform.Translate(Vector3.up * upSpeed * 0.6f * Time.deltaTime);
        }
    }

    
    public void Knockback(Vector2 direction)
    {
        // Calculate the knockback force vector.
        Vector2 knockVector = direction.normalized * knockBackForce;

        // Apply the knockback force.
        StartCoroutine(ApplyKnockback(knockVector));
    }

    public IEnumerator ApplyKnockback(Vector2 knockbackVector)
    {   
        // Apply the knockback force to the player.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = knockbackVector;

        yield return new WaitForSeconds(knockbackDuration);

        // Reset the velocity to zero after the knockback duration
        rb.velocity = Vector2.zero;

    }

    public void DisableMovement()
    {
        enabled = false;
    }
}