using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] GameObject deathFx;
    [SerializeField] bool isVulnerable = true;
    [SerializeField] float invulnerabilityTime = 0.5f;
    [SerializeField] float blinkTime = 0.25f;
    [SerializeField] Color blinkTo = Color.red;

    Color original;
    SpriteRenderer rend;
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        original = rend.color;
    }

    private void Die()
    {
        GameObject go = Instantiate(deathFx, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }
    
    private IEnumerator InvulnerabilityRoutine()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isVulnerable = true;
    }

    private IEnumerator BlinkDamage()
    {
        rend.color = blinkTo;
        yield return new WaitForSeconds(blinkTime);
        rend.color = original;
        yield return new WaitForSeconds(blinkTime);
        rend.color = blinkTo;
        yield return new WaitForSeconds(blinkTime);
        rend.color = original;
    }


    public void TakeDamage(int damage)
    {
        if(!isVulnerable) { return; }

        isVulnerable = false;
        currentHealth -= damage;
        StartCoroutine(BlinkDamage());

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }
}
