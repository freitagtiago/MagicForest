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

        AudioSource.PlayClipAtPoint(audios[audioIndex], Camera.main.transform.position);
    }

    private IEnumerator UnlockExit()
    {
        AudioSource.PlayClipAtPoint(unlockAudio, Camera.main.transform.position);
        particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(timeToWait);
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
