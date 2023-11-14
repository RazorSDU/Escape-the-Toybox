using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int maxHealth = 3;
    private int currentHealth;
    public Text healthText;
    private Color orange = new Color(1f, 0.5f, 0f);

    public Slider healthSlider;
    public Canvas gameOverCanvas;
    public Button retryButton;
    public GameObject gameOverEmpty;
    public Animator animator;
    public GameObject VictoryEmpty;
    public Canvas victoryCanvas;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverEmpty != null)
        {
            gameOverEmpty.gameObject.SetActive(false);
        }
        if (VictoryEmpty != null)
        {
            VictoryEmpty.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Danger"))
        {
            if (!playerMovement.isInvincible)
            {
                // Deduct health when the player touches an object with the "Danger" layer
                TakeDamage();

                // Blinking effect
                StartCoroutine(BlinkEffect());

                if(currentHealth > 0) { 
                    // Does a knockback on the player
                    playerMovement.Knockback(collision.contacts[0].normal);
                }
            }

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Victory"))
        {
            ShowVictoryScreen();

        }
    }
    public void ShowVictoryScreen()
    {
        //Show the Game Over Canvas
        if (VictoryEmpty != null)
        {
            //Debug.Log("Game Over Screen Activated!");
            VictoryEmpty.gameObject.SetActive(true);
            playerMovement.DisableMovement();
        }
    }
    public void ShowGameOverScreen()
    {
        //Show the Game Over Canvas
        if (gameOverEmpty != null)
        {
            //Debug.Log("Game Over Screen Activated!");
            gameOverEmpty.gameObject.SetActive(true);
        }
    }

    void TakeDamage()
    {
        currentHealth--;
        UpdateHealthUI();
    }

    public void RetryButtonClicked()
    {
        // Reload the scene or perform any other action to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //playerMovement.EnableMovement();
    }


    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
            healthText.text = $"Health: {currentHealth}";

            Image handleImage = healthSlider.handleRect.GetComponentInChildren<Image>();

            if (currentHealth == 2)
            {
                healthText.color = Color.yellow;
                handleImage.color = Color.yellow;
            }
            else if (currentHealth == 1)
            {
                healthText.color = orange;
                handleImage.color = orange;
            }
            else if (currentHealth <= 0)
            {
                currentHealth = 0;
                videoPlayer.Pause();
                animator.StopPlayback();
                animator.SetBool("Dead", true);
                healthText.color = Color.red;
                handleImage.color = Color.red;
                ShowGameOverScreen();
                healthText.text = $"YOU ARE DEAD!";
                playerMovement.DisableMovement();
                playerMovement.invincibilityDuration = 0f;
            }

        }
    }

    IEnumerator BlinkEffect()
    {
        playerMovement.isInvincible = true;

        // Get the SpriteRenderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Enable blinking for the invincibility duration
        StartCoroutine(Blink(spriteRenderer));

        // Wait for the invincibility duration
        yield return new WaitForSeconds(playerMovement.invincibilityDuration);

        // Disable blinking
        StopAllCoroutines();
        spriteRenderer.enabled = true;

        playerMovement.isInvincible = false;
    }

    IEnumerator Blink(SpriteRenderer spriteRenderer)
    {
        while (true && currentHealth > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f); // Adjust the blink speed as needed
        }
    }
}
