using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleFromMic : MonoBehaviour
{
    public AudioSource src;
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;
    public Slider sensitivitySlider;
    public Slider threshold;

    void Update() {
        float loudness = detector.GetLoudnessFromInput() * sensitivitySlider.value;

        if (loudness < threshold.value)
            loudness = 0;

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
