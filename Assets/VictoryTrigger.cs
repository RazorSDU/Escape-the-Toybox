using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public GameObject VictoryEmpty;
    public Button retryButton;
    public Canvas victoryScreen;
    // Start is called before the first frame update
    void Start()
    {
        if (VictoryEmpty != null)
        {
            VictoryEmpty.gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
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
        }
    }
}