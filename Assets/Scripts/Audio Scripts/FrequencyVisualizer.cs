using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyVisualizer : MonoBehaviour
{
    public int band;
    public float startScale, scaleMulti;

    void Update() {
        transform.localScale = new Vector3(
            transform.localScale.x,
            (VisualPreloadedClip.frequencyBands[band] * scaleMulti) + startScale,
            transform.localScale.z
        );
    }
}
