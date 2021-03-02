using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXPlayer : MonoBehaviour
{
    public static AudioFXPlayer fxPlayer;
    AudioSource audioPlayer;

    private void Awake()
    {
        if(fxPlayer != null)
        {
            Destroy(this);
        }
        else
        {
            fxPlayer = this;
        }
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlaySoundOnCamera(AudioClip audio)
    {
        audioPlayer.PlayOneShot(audio);
    }
}
