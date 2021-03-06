using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayer : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            player.TakeDamage(damage);
        }
    }
}
