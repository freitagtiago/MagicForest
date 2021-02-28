using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBox : MonoBehaviour
{
    [SerializeField] float impulse = 2.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Mover player = other.GetComponent<Mover>();
        if (player)
        {
            player.SpecialJump(impulse);
        }        
    }
}
