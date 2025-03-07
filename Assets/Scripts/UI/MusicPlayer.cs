using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    [SerializeField] List<AudioClip> _musicList;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic(0);
    }

    public void PlayMusic(int index)
    {
        if (_audioSource.isPlaying) 
        { 
            _audioSource.Stop(); 
        }
        
        _audioSource.clip = _musicList[index];
        _audioSource.Play();
    }
}
