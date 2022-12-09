using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControls : MonoBehaviour
{
    [Header("red")]
    public Slider sensitivtyScaleSlide;
    [Range(0, 100)] public int s = 50;
    public static int r_sensitivity = 100;

    void Start() {
        sensitivtyScaleSlide.onValueChanged.AddListener((val) => {
            r_sensitivity = s;
        });
    }
}
