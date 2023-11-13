using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallingObstacleSpawner : MonoBehaviour
{
    public GameObject fallingObstaclePrefab;
    public float moveSpeed = 10.0f;
    public GameObject player;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void SpawnFallingObstacle()
    {
        // Randomly generate a position within the view of the main camera
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate a new falling obstacle at the spawn position
        GameObject fallingObstacle = Instantiate(fallingObstaclePrefab, spawnPosition, Quaternion.identity);

        // Start a coroutine for the new obstacle's lifecycle
        StartCoroutine(MoveAndDespawnObstacle(fallingObstacle));
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = player.transform.position; // Assuming you have a reference to the player object
        float spawnRadius = 15.0f;

        Vector3 spawnPosition;

        do
        {
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);
            float randomRadius = Random.Range(0f, spawnRadius);

            float spawnX = playerPosition.x + randomRadius * Mathf.Cos(randomAngle);
            float spawnY = playerPosition.y + randomRadius * Mathf.Sin(randomAngle);

            spawnPosition = new Vector3(spawnX, spawnY, 19);
        } while (IsCollidingWithWall(spawnPosition));

        return spawnPosition;
    }

    private bool IsCollidingWithWall(Vector3 position)
    {
        // Check for collisions with objects on the "WallColliders" layer within a small radius
        Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.1f, LayerMask.GetMask("WallColliders"));

        // If there is a collision, return true; otherwise, return false
        return hitCollider != null;
    }


    private IEnumerator MoveAndDespawnObstacle(GameObject obstacle)
    {
        // Initialize variables for scaling and positioning
        float moveSpeedMultiplier = 2f;
        float durationMultiplier = 8f;
        float scaleTime = 0.0f;
        Vector3 initialScale = Vector3.one; // Initial scale set to (1, 1, 1)
        Vector3 targetScale = new Vector3(2.5f, 2.5f, 1);
        Transform obstacleTransform = obstacle.transform;
        Vector3 moveDirection = Vector3.up;

        // Move and scale the obstacle over time
        while (scaleTime < (5f / 11f) * durationMultiplier)
        {
            obstacleTransform.position += moveDirection * moveSpeed * Time.deltaTime;
            obstacleTransform.localScale = Vector3.Lerp(initialScale, targetScale, scaleTime / ((5f / 11f) * moveSpeedMultiplier));
            scaleTime += Time.deltaTime;

            if (scaleTime >= (5f / 11f) * moveSpeedMultiplier)
            {
                // Change obstacle color to red
                SpriteRenderer spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.red;
            }

            if (scaleTime >= (5f / 11f) * durationMultiplier)
            {
                // Destroy the obstacle after scaling and moving
                Destroy(obstacle);
            }

            yield return null;
        }
    }
}