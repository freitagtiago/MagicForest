using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _invulnerabilityTime = 0.6f;
    [SerializeField] private float _damagedTime = 0.3f;
    [SerializeField] private AudioClip _audioHurt;

    private CapsuleCollider2D _playerCollider;
    private Animator _animator;
    private UIHandler _uiHandler;
    private SpriteRenderer _renderer;

    private bool _isAlive = true;
    private bool _isVulnerable = true;

    private void Awake()
    {
        _playerCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _uiHandler = FindObjectOfType<UIHandler>();
    }

    private void Start()
    {
        _uiHandler?.UpdateHealth(_currentHealth);
    }

    private void Update()
    {
        if (_isAlive)
        {
            if (_playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
            {
                Die();
            }
        }
    }

    private void EnableDisableRenderer(bool value)
    {
        if (_isAlive)
        {
            _renderer.enabled = value;
        }
        else
        {
            _renderer.enabled = false;
        }
        
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _isVulnerable = true;
    }

    private IEnumerator DamagedRoutine()
    {
        for(int i = 0; i<= 5; i++)
        {
            EnableDisableRenderer(!_renderer.enabled);
            yield return new WaitForSeconds(_damagedTime);
        }
        EnableDisableRenderer(true);
    }

    public void TakeDamage(int damage)
    {
        if(!_isVulnerable) { return; }

        _isVulnerable = false;
        StartCoroutine(DamagedRoutine());
        StartCoroutine(InvulnerabilityRoutine());
        if (_currentHealth <= 1)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(_audioHurt, Camera.main.transform.position);
            _currentHealth--;
        }
        _uiHandler.UpdateHealth(_currentHealth);
    }

    public void Die()
    {
        _isAlive = false;
        GetComponent<Mover>().SetCanMove(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Instantiate(_deathFx, transform.position, Quaternion.identity);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
    
    public void Heal(int value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + value, _currentHealth, _maxHealth);
        _uiHandler.UpdateHealth(_currentHealth);
    }
}
