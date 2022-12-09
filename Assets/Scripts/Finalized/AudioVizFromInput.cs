using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioVizFromInput : MonoBehaviour
{
    [System.Serializable]
    public struct VizInput {
        public GameObject vizCube;
        public string micName;  // name of curr input
        public AudioClip micClip;  // Clip of curr input
        public Slider sensitivity;
        public Slider threshold;
        public TMP_Dropdown sources;
        public AudioSource audioSrc;
    }

    public VizCube inp1;
    public VizCube inp2;

    // Values accessible by both [all] visual cubes
    public int sampleWindow = 64;
    public int totSrcs;
    public Vector3 minScale;
    public Vector3 maxScale;
    public TextMeshProUGUI numSrcs;
    private List<string> options;

    void Start() {
        totSrcs = Microphone.devices.Length;
        numSrcs.text = "NUM SOURCES: " + totSrcs;

        options = Microphone.devices.ToList<string>();
        inp1.sources.AddOptions(options);
        inp2.sources.AddOptions(options);

        inp1.micName = options[0];
        inp2.micName = options[0];

        inp1.sources.onValueChanged.AddListener((newName) => {
            inp1.micName = options[newName];
            MicrophoneToAudioClip(inp1);
        });

        inp2.sources.onValueChanged.AddListener((newName) => {
            inp2.micName = options[newName];
            MicrophoneToAudioClip(inp2);
        });

        
    }

    void Update() {
        Detector(inp1);
        Detector(inp2);
    }

    // Helpers
    public void MicrophoneToAudioClip(VizCube v) {
        //Debug.Log("Mic Name: " + v.micName);
        v.micClip = Microphone.Start(v.micName, true, 20, AudioSettings.outputSampleRate);
    }

    private float GetLoudnessFromInput(VizCube v) {
        return GetLoudnessFromClip(Microphone.GetPosition(v.micName), v.micClip);
    }

    private float GetLoudnessFromClip(int clipPosition, AudioClip clip) {
        int startPosition = clipPosition - sampleWindow;

        if(startPosition < 0) { return 0; }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData,startPosition);

        // compute loudness
        float totLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totLoudness += Mathf.Abs(waveData[i]);
        }

        return totLoudness / sampleWindow;
    }

    private void Detector(VizCube v) {
        float loudness = GetLoudnessFromInput(v) * v.sensitivity.value;

        //Debug.LogWarning("Loudness: " + loudness);

        if (loudness < v.threshold.value) {
            loudness = 0;
        }

        v._self.transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
