using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class AudioSourceManager : MonoBehaviour
{
    public TextMeshProUGUI numSrc;
    public TMP_Dropdown srcs1;
    public TMP_Dropdown srcs2;
    public static string mic1Name;
    public static string mic2Name;
    private List<string> options;

    void Start() {
        numSrc.text = "NUMBER OF SOURCES: " + Microphone.devices.Length;
        options = Microphone.devices.ToList<string>();
        Debug.Log(options.Count);
        srcs1.AddOptions(options);
        srcs2.AddOptions(options);

        srcs1.onValueChanged.AddListener((newName) => {
            mic1Name = options[newName];
        });

        srcs2.onValueChanged.AddListener((newName) => {
            mic2Name = options[newName];
        });
    }
}
