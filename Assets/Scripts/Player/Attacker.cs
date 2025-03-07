using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private float _cooldownTime = 1.5f;
    [SerializeField] private GameObject _attackPrefab;
    [SerializeField] private Transform _handPos;
    [SerializeField] private AudioClip _attackSound;
    private BoxCollider2D _feetCollider;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _feetCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(_canAttack 
            && IsGrounded() 
            && Input.GetKey(KeyCode.N))
        {
            StartCoroutine(AttackCooldown());
            DoAttack();
        }
    }

    private bool IsGrounded()
    {
        return _feetCollider.IsTouchingLayers(LayerMask.GetMask("Walkable"));
    }

    private void DoAttack()
    {
        _animator.SetBool("walking", false);
        _animator.SetBool("running", false);
        _animator.SetTrigger("slashing");

        AudioSource.PlayClipAtPoint(_attackSound, Camera.main.transform.position);
        GameObject go = Instantiate(_attackPrefab, _handPos.position, Quaternion.identity);
        go.transform.parent = _handPos;
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_cooldownTime);
        _canAttack = true;
    }

    public void SetCanAttack(bool value)
    {
        _canAttack = value;
    }
}
