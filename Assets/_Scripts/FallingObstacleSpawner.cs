using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallingObstacleSpawner : MonoBehaviour
{
    public GameObject fallingObstaclePrefab;
    public GameObject fallingobstacleLine;
    public float moveSpeed = 10.0f;
    public GameObject player;

    public float offsetX = 0.0f; // Adjust the X offset as needed
    public float offsetY = 0.0f; // Adjust the Y offset as needed

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void SpawnFallingObstacle(float radius)
    {
        // Randomly generate a position within the view of the main camera
        Vector3 spawnPosition = GetRandomSpawnPosition(radius);
        Vector3 newDirection = ChangeFallDirectionRandom();

        // Instantiate a new falling obstacle at the spawn position
        GameObject fallingObstacleLine = Instantiate(fallingobstacleLine, spawnPosition + new Vector3(offsetX, offsetY, 0), Quaternion.identity);

        fallingObstacleLine.transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
        GameObject fallingObstacle = Instantiate(fallingObstaclePrefab, spawnPosition, Quaternion.identity);

        // Start a coroutine for the new obstacle's lifecycle
        StartCoroutine(MoveAndDespawnObstacle(fallingObstacle, fallingObstacleLine, newDirection));
    }

    private Vector3 GetRandomSpawnPosition(float spawnRadius)
    {
        Vector3 playerPosition = player.transform.position; // Assuming you have a reference to the player object

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


    private IEnumerator MoveAndDespawnObstacle(GameObject obstacle, GameObject fallingObstacleLine, Vector3 newDirection)
    {
        // Initialize variables for scaling and positioning
        float moveSpeed = 2f;
        float duration = 4f;
        float scaleTime = 0.0f;
        Vector3 initialScale = Vector3.one; // Initial scale set to (1, 1, 1)
        Vector3 targetScale = new Vector3(2.5f, 2.5f, 1);
        Transform obstacleTransform = obstacle.transform;
        Transform fallingObstacleLineTransform = fallingObstacleLine.transform;
        Vector3 moveDirection = Vector3.up;
        // Move and scale the obstacle over time
        while (scaleTime < (5f / 11f) * duration * 2.5f)
        {

            if (scaleTime >= (5f / 11f) * duration * 2f - (duration * 0.2f))
            {
                // Change obstacle color to red
                SpriteRenderer spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // Red with 40% transparency

                // Change obstacle layer to "Danger"
                obstacle.layer = LayerMask.NameToLayer("Danger");

                obstacleTransform.transform.Translate((newDirection * 4f) * Time.deltaTime);
                scaleTime += Time.deltaTime;

                Destroy(fallingObstacleLine);
            }
            else
            {
                fallingObstacleLineTransform.position += moveDirection * moveSpeed * Time.deltaTime;
                scaleTime += Time.deltaTime;

                obstacleTransform.position += moveDirection * moveSpeed * Time.deltaTime;
                obstacleTransform.localScale = Vector3.Lerp(initialScale, targetScale, scaleTime/4f);
                scaleTime += Time.deltaTime;
            }

            if (scaleTime >= (5f / 11f) * duration * 2.5f)
            {
                // Destroy the obstacle after scaling and moving
                Destroy(obstacle);
            }

            yield return null;
        }
    }

    private Vector3 ChangeFallDirectionRandom()
    {
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;

        return newDirection;
    }

}