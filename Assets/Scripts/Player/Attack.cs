using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void Start()
    {
        FlipSprite();
    }

    private void Update()
    {
        FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(1, 1, 0);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
