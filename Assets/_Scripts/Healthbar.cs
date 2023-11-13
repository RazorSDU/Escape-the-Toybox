using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Text healthText;
    private Color orange = new Color(1f, 0.5f, 0f);

    public Slider healthSlider;
    public Canvas gameOverCanvas;
    public Button retryButton;
    public GameObject gameOverEmpty;

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Danger"))
        {
            // Does a knockback on the player
            playerMovement.Knockback(collision.contacts[0].normal);

            // Deduct health when the player touches an object with the "Danger" layer
            TakeDamage();
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
                healthText.color = Color.red;
                handleImage.color = Color.red;
                ShowGameOverScreen();
                healthText.text = $"YOU ARE DEAD!";
                playerMovement.DisableMovement();
            }

        }
    }
}
