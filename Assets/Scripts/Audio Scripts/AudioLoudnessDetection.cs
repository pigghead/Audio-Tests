using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    public static AudioClip micClip;
    public static AudioClip mic2Clip;
    public static string micName;
    public static string mic2Name;
    public TMP_Dropdown srcs;
    public TextMeshProUGUI numSrc;
    private static List<string> options;
    public AudioSourceManager srcMan;

    void Start() {
        MicrophoneToAudioClip();

        numSrc.text = "NUMBER OF SOURCES: " + Microphone.devices.Length;
        options = Microphone.devices.ToList<string>();

        srcs.onValueChanged.AddListener((newName) => {
            micName = options[newName];
        });
    }

    public static void MicrophoneToAudioClip() {
        micClip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromInput() {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), micClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip) {
        int startPosition = clipPosition - sampleWindow;

        if(startPosition < 0) {return 0;}

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData,startPosition);

        // compute loudness
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
