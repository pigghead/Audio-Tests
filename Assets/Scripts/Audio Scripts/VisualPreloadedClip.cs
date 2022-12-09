using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class VisualPreloadedClip : MonoBehaviour
{
    private AudioSource src;
    public static float[] samples;
    public static float[] frequencyBands;

    void Start() {
        src = GetComponent<AudioSource>();
        samples = new float[512];
        frequencyBands = new float[8];
    }

    void Update() {
        GetSpectrum();
        MakeFrequencyBands();

        if(Input.GetKeyDown(KeyCode.Space)) {
            if(src.isPlaying) { 
                src.Pause(); 
            } else {
                src.UnPause();
            }
        }
    }

    private void GetSpectrum() {
        src.GetSpectrumData(samples, 0, FFTWindow.Hanning);
    }

    private void MakeFrequencyBands() {
        int count = 0;

        for (int i = 0; i < 8; i++) {
            float sampleCount = Mathf.Pow(2, i) * 2;
            float avgSamples = 0f;
            if(i==7) {
                sampleCount +=2;
            }

            for(int j = 0; j < sampleCount; j++) {
                avgSamples += samples[count] * (count + 1);
                count++;
            }

            avgSamples /= count;
            frequencyBands[i] = avgSamples * 10;
        }
    }
}
