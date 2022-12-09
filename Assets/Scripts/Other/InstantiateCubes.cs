using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject pf_visualCube;
    public float maxScale;
    private GameObject[] pf_Cubes = new GameObject[512];

    void Start() {
        for(int i = 0; i < 512; i++) {
            GameObject _cube = Instantiate(pf_visualCube);
            _cube.transform.position = this.transform.position;
            _cube.transform.parent = this.transform;
            _cube.name = "Visual Cube " + i;
            this.transform.eulerAngles = new Vector3 (0, 0.703125f * i, 0);
            _cube.transform.position  = Vector3.forward * 100;
            pf_Cubes[i] = _cube;
        }
    }

    void Update() {
        for(int i = 0; i < 512; i ++) {
            if(pf_Cubes != null) {
                pf_Cubes[i].transform.localScale = new Vector3(1, (VisualPreloadedClip.samples[i] * maxScale), 1); 
            }
        }
    }

    private void Spawn() {

    }
}
