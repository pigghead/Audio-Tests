using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class VizCube : MonoBehaviour
{
    public GameObject _self;
    public string micName;
    public AudioSource src;
    public AudioClip micClip;
    public Slider sensitivity;
    public Slider threshold;
    public TMP_Dropdown sources;

    void Awake() {
        _self = gameObject;
        //Debug.Log(Microphone.GetPosition(micName));
    }
}
