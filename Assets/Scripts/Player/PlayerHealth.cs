using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] float invulnerabilityTime = 0.6f;
    [SerializeField] float damagedTime = 0.3f;
    [SerializeField] AudioClip audioHurt;

    CapsuleCollider2D playerCollider;
    Animator anim;
    UIHandler ui;
    SpriteRenderer rend;

    bool isAlive = true;
    bool isVulnerable = true;

    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        ui = FindObjectOfType<UIHandler>();
    }

    private void Start()
    {
        ui?.UpdateHealth(currentHealth);
    }

    private void Update()
    {
        if (isAlive)
        {
            if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
            {
                Die();
            }
        }
    }

    private void EnableDisableRenderer(bool value)
    {
        if (isAlive)
        {
            rend.enabled = value;
        }
        else
        {
            rend.enabled = false;
        }
        
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isVulnerable = true;
    }

    private IEnumerator DamagedRoutine()
    {
        for(int i = 0; i<= 5; i++)
        {
            EnableDisableRenderer(!rend.enabled);
            yield return new WaitForSeconds(damagedTime);
        }
        EnableDisableRenderer(true);
    }

    public void TakeDamage(int damage)
    {
        if(!isVulnerable) { return; }

        isVulnerable = false;
        StartCoroutine(DamagedRoutine());
        StartCoroutine(InvulnerabilityRoutine());
        if (currentHealth <= 1)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(audioHurt, Camera.main.transform.position);
            currentHealth--;
        }
        ui.UpdateHealth(currentHealth);
    }

    public void Die()
    {
        isAlive = false;
        GetComponent<Mover>().SetCanMove(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Instantiate(deathFx, transform.position, Quaternion.identity);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
    
    public void Heal(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, currentHealth, maxHealth);
        ui.UpdateHealth(currentHealth);
    }
}
