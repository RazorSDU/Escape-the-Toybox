using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacleSpawner : MonoBehaviour
{
    public GameObject fallingObstaclePrefab;
    public float spawnInterval = 6.0f;
    public float moveSpeed = 10.0f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnObstaclesPeriodically());
    }

    private IEnumerator SpawnObstaclesPeriodically()
    {
        while (true)
        {
            // Randomly generate a position within the view of the main camera
            float randomX = Random.Range(0, Screen.width);
            float randomY = Random.Range(0, Screen.height);
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(randomX, randomY, 19));

            // Instantiate a new falling obstacle at the spawn position
            GameObject fallingObstacle = Instantiate(fallingObstaclePrefab, spawnPosition, Quaternion.identity);

            // Start a coroutine for the new obstacle's lifecycle
            StartCoroutine(MoveAndDespawnObstacle(fallingObstacle));

            // Wait for the specified spawn interval before spawning the next obstacle
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveAndDespawnObstacle(GameObject obstacle)
    {
        float scaleTime = 0.0f;
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = new Vector3(2.5f, 2.5f, 1);
        Transform obstacleTransform = obstacle.transform;

        while (scaleTime < 2.0f)
        {
            // Interpolate the scale
            obstacleTransform.localScale = Vector3.Lerp(initialScale, targetScale, scaleTime / 2.0f);

            scaleTime += Time.deltaTime;
            yield return null;
        }

        SpriteRenderer spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;

        Vector3 initialPosition = obstacleTransform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * moveSpeed * 2.0f; // Move for 1 second

        float elapsedTime = 0.0f;
        while (elapsedTime < 2.0f)
        {
            obstacleTransform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / 2.0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the obstacle
        Destroy(obstacle);
    }
}
