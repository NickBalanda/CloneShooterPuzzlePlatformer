using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour{

    public AudioSource ausMusic;
    public AudioSource ausSFX;

    public AudioClip[] audioClips;

    public static MusicManager instance;
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void PlaySound(string clipName) {
        foreach (var item in audioClips) {
            if(item.name == clipName) {
                ausSFX.PlayOneShot(item);
            }
        }       
    }
    public void PauseMusic(bool value) {
        if (value)
            ausMusic.Pause();
        else
            ausMusic.UnPause();
    }
}
