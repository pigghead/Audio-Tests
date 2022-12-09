using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader _instance;
    private int sceneNum;

    void Start() {
        // Singleton instantiation
        _instance = this;
        DontDestroyOnLoad(this);

        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(1);
        }
    }
}
