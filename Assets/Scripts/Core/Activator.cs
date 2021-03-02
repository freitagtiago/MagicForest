using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] bool active = false;
    [SerializeField] AudioClip audio;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active) { return; }
        PushableBox box = other.GetComponent<PushableBox>();
        if (box) 
        {
            active = true;
            //AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
            AudioFXPlayer.fxPlayer.PlaySoundOnCamera(audio);
            ParticleSystem particles = GetComponent<ParticleSystem>();
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            FindObjectOfType<Quests>().CompleteStep();
            Destroy(this);
        }    
    }
}
