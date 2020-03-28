using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    void Start() {

    }

    private void Update() {
        if (Input.GetButton("Submit")) {
            LoadFirstScene();
        }
    }

    void LoadFirstScene() {
        SceneManager.LoadScene(1);
    }
}
