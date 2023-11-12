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
    }

    public Transform targetObject;
    public PositionRotationKeyframe[] keyframes;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI currentTimeText; // Reference to the Text component for displaying current time

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

            targetObject.rotation = Quaternion.Slerp(
                keyframes[currentKeyframeIndex].rotation,
                keyframes[currentKeyframeIndex + 1].rotation,
                t
            );

            // Display the current time on the UI
            if (currentTimeText != null)
            {
                currentTimeText.text = "Current Time: " + currentTime.ToString("F2");
            }
        }
    }
}