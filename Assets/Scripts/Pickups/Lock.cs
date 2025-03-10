using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] UnityEvent unlockExit;
    [SerializeField] int collectedPurpleCoins = 0;
    [SerializeField] int totalAmountOfPurpleCoins;
    [SerializeField] float timeToWait = 2f;
    [SerializeField] AudioClip[] audios;
    [SerializeField] AudioClip unlockAudio;
    [SerializeField] ParticleSystem particles;

    private void Start()
    {
        totalAmountOfPurpleCoins = transform.childCount;
    }

    private void PlayAudioFX()
    {
        int audioIndex = collectedPurpleCoins - 1;

        AudioFXPlayer.Instance.PlaySoundOnCamera(audios[audioIndex]);
    }

    private IEnumerator UnlockExit()
    {
        AudioFXPlayer.Instance.PlaySoundOnCamera(unlockAudio);
        particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSecondsRealtime(timeToWait);
        unlockExit.Invoke();
    }

    public void Collect()
    {
        collectedPurpleCoins++;
        PlayAudioFX();
        if(collectedPurpleCoins == totalAmountOfPurpleCoins)
        {
            StartCoroutine(UnlockExit());
        }
    }
}
