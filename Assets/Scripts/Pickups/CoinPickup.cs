using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip soundPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            //AudioSource.PlayClipAtPoint(soundPickup, Camera.main.transform.position);
            AudioFXPlayer.fxPlayer.PlaySoundOnCamera(soundPickup);
            FindObjectOfType<GameSession>().AddCoins();
            Destroy(this.gameObject);
        }
    }
}
