using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] AudioClip recoveryAudio;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerHealth>()?.Heal(amount);
        AudioFXPlayer.Instance.PlaySoundOnCamera(recoveryAudio);
        //AudioSource.PlayClipAtPoint(recoveryAudio, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
