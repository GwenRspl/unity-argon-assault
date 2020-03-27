using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    private void Awake() {
        int nbMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (nbMusicPlayers > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

}
