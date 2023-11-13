using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class VideoSync : MonoBehaviour
{
    [System.Serializable]
    public class PositionRotationKeyframe
    {
        public float time;
        public Vector3 position;
        public Quaternion rotation;

        public void NormalizeRotation()
        {
            rotation.Normalize();
        }
    }

    public Transform targetObject;
    public PositionRotationKeyframe[] keyframes;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI currentTimeText; // Reference to the Text component for displaying current time
    public Camera mainCamera; // Reference to the main camera

    private int currentKeyframeIndex = 0;

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            float currentTime = (float)videoPlayer.time;

            // Find the correct keyframe based on the current time
            while (currentKeyframeIndex < keyframes.Length - 1 && currentTime >= keyframes[currentKeyframeIndex + 1].time)
            {
                currentKeyframeIndex++;
            }
            while (currentKeyframeIndex > 0 && currentTime < keyframes[currentKeyframeIndex].time)
            {
                currentKeyframeIndex--;
            }

            // Calculate interpolation factor (t) based on the current keyframe and next keyframe
            float t = Mathf.InverseLerp(
                keyframes[currentKeyframeIndex].time,
                keyframes[currentKeyframeIndex + 1].time,
                currentTime
            );

            // Interpolate and set the position and rotation
            targetObject.position = Vector3.Lerp(
                keyframes[currentKeyframeIndex].position,
                keyframes[currentKeyframeIndex + 1].position,
                t
            );

            // Calculate the pivot point based on the middle of the camera's viewport
            Vector3 cameraMiddlePoint = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane));
            Vector3 pivotRelative = targetObject.position - cameraMiddlePoint;

            keyframes[currentKeyframeIndex].NormalizeRotation();
            keyframes[currentKeyframeIndex + 1].NormalizeRotation();

            targetObject.rotation = Quaternion.SlerpUnclamped(
                keyframes[currentKeyframeIndex].rotation,
                keyframes[currentKeyframeIndex + 1].rotation,
                t
            );

            // Adjust the position after rotation to maintain the same distance from the pivot point
            targetObject.position = cameraMiddlePoint + targetObject.rotation * pivotRelative;

            Debug.Log($"Pos: {targetObject.position}, Rot: {targetObject.rotation}");

            // Display the current time on the UI
            if (currentTimeText != null)
            {
                currentTimeText.text = "Current Time: " + currentTime.ToString("F2");
            }
        }
    }
}