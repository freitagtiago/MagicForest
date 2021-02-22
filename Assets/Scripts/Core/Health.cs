using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    CapsuleCollider2D playerCollider;
    bool isAlive = true;

    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (isAlive)
        {
            if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
            {
                Die();
            }
        }
        
    }

    public void Die()
    {
        isAlive = false;
        GetComponent<Mover>().SetCanMove(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Instantiate(deathFx, transform.position, Quaternion.identity);
    }
    
}
