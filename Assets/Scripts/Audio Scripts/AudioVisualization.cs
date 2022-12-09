using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualization : MonoBehaviour
{
    public AudioSource _src;
    public AudioClip _micClip;
    private int _sampleWindow = 64;
    public float[] _samples;

    void Start() {
        ClipFromMicrophone();
        _src = GetComponent<AudioSource>();
        _samples = new float[64];
        //_clip = _src.clip;

        //_src.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void Update() {
        //ClipFromMicrophone();
        _samples = GetFloatFromClip(Microphone.GetPosition(Microphone.devices[0]), _micClip);
    }

    private void ClipFromMicrophone() {
        string micName = Microphone.devices[0];
        _micClip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate);
    }

    private float[] GetFloatFromClip(int clipPos, AudioClip clip) {
        int startPos = clipPos - _sampleWindow;

        if (startPos < 0) {
            return new float[0];
        }

        // PREDICTION:: _sampleWindow will be too small
        float[] waveData = new float[_sampleWindow];
        clip.GetData(waveData, startPos);

        /*for(int i = 0; i < 64; i++) {
            _samples[i] = waveData[i];
        }*/

        return waveData;
    }
    // RESULT:: Empty float[] returned?
}
