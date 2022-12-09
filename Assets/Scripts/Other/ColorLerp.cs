using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public Color startCol = Color.red;
    public Color endCol = Color.green;
    float dur = 8.0f;
    Renderer rend;

    void Start() {
        rend = gameObject.GetComponent<Renderer>();
    }

    void Update() {
        float lerp = Mathf.PingPong(Time.time, dur) / dur;
        rend.material.color = Color.Lerp(startCol, endCol, lerp);
    }
}
