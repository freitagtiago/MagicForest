using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] List<AudioClip> musicList;

    public static MusicPlayer instance;
    AudioSource audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic(0);
    }

    public void PlayMusic(int index)
    {
        if (audioSource.isPlaying) { audioSource.Stop(); }
        
        audioSource.clip = musicList[index];
        audioSource.Play();
    }
}
