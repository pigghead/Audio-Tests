using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Transform _self;
    private float RotationSpeedY = 20f;
    private float RotationSpeedX = 12f;

    void Start() {
        _self = gameObject.transform;
    }

    void Update() {
        _self.Rotate(Vector3.up * (RotationSpeedY * Time.deltaTime));
        _self.Rotate(Vector3.right * (RotationSpeedX * Time.deltaTime));
    }
}
