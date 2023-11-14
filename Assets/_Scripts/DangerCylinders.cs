using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DangerCylinders : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    [System.Serializable]
    public class MovementParameters
    {
        public Transform objectToMove;
        public Vector2 startPoint;
        public Vector2 endPoint;
        public float startTime;
        public float endTime;
    }
    public MovementParameters movementParams;
    private bool isMoving = false;

    private bool isCoroutineRunning = false;
    bool hasRun = false;
    public VideoPlayer videoPlayer;

    void Start()
    {
        SetObjectTransparent(object1, 0f);
        SetObjectTransparent(object2, 0f);
        SetObjectTransparent(object3, 0f);
    }

    void Update()
    {
        float currentTime = (float)videoPlayer.time;
        if (currentTime >= 48.5f && currentTime <= 51f && !hasRun)
        {
            startRoll();
            hasRun = true;
        }

        //if (currentTime >= movementParams.startTime && currentTime <= movementParams.endTime)
        //{
        //    if (!isMoving)
        //    {
        //        isMoving = true;
        //    }

        //    float t = (currentTime - movementParams.startTime) / (movementParams.endTime - movementParams.startTime);
        //    movementParams.objectToMove.position = Vector2.Lerp(movementParams.startPoint, movementParams.endPoint, t);
        //}
        //else if (isMoving)
        //{
        //    isMoving = false;
        //}

        //// Check if the current time is within the specified range
        //if (Time.time >= moveSettings.startTime && Time.time <= moveSettings.endTime)
        //{
        //    // Calculate the normalized time between startTime and endTime
        //    float normalizedTime = Mathf.InverseLerp(moveSettings.startTime, moveSettings.endTime, Time.time);

        //    // Interpolate the position between startPoint and endPoint based on the normalized time
        //    Vector2 newPosition = Vector2.Lerp(moveSettings.startPoint, moveSettings.endPoint, normalizedTime);

        //    // Set the object's position to the interpolated position
        //    transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        //    // Update elapsed time
        //    elapsedTime = Time.time - moveSettings.startTime;
        //}
    }

    

    public void startRoll()
    {
        // Start the transparency animation
        StartCoroutine(TransparentAnimation());
    }

    IEnumerator TransparentAnimation()
    {
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;

            // Step 1: GameObject 1 goes to 60% transparent over 5/11 seconds
            StartCoroutine(FadeObject(object1, 0.6f, 5f / 11f));

            // Step 2: After 2 seconds, GameObject 1 goes instantly to 0% transparent
            yield return new WaitForSeconds(2.5f);
            SetObjectTransparent(object1, 0f);

            // Step 3: GameObject 2 and 3 go to 60% transparent over 5/11 seconds
            StartCoroutine(FadeObject(object2, 0.6f, 5f / 11f));
            StartCoroutine(FadeObject(object3, 0.6f, 5f / 11f));

            // Step 4: After 2 seconds, GameObject 2 and 3 go instantly to 0% transparent
            yield return new WaitForSeconds(2.5f);
            SetObjectTransparent(object2, 0f);
            SetObjectTransparent(object3, 0f);

            isCoroutineRunning = false;
        }
    }

    IEnumerator FadeObject(GameObject obj, float targetAlpha, float duration)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        // Check if SpriteRenderer component exists
        if (spriteRenderer != null)
        {
            float startAlpha = spriteRenderer.color.a;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
                SetObjectTransparent(obj, newAlpha);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure that the final alpha is precisely the target alpha
            SetObjectTransparent(obj, targetAlpha);
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on GameObject: " + obj.name);
        }
    }

    void SetObjectTransparent(GameObject obj, float alpha)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }
}
