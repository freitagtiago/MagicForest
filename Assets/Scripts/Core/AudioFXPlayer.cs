using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXPlayer : MonoBehaviour
{
    public static AudioFXPlayer Instance;
    private AudioSource _audioPlayer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _audioPlayer = GetComponent<AudioSource>();
    }

    public void PlaySoundOnCamera(AudioClip audio)
    {
        _audioPlayer.PlayOneShot(audio);
    }
}
