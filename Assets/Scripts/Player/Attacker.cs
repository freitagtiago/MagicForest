using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] bool canAttack = true;
    [SerializeField] float cooldownTime = 1.5f;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Transform handPos;
    [SerializeField] AudioClip attackSound;
    BoxCollider2D feetCollider;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(canAttack && IsGrounded() &&Input.GetKey(KeyCode.N))
        {
            StartCoroutine(AttackCooldown());
            DoAttack();
        }
    }

    private bool IsGrounded()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Walkable"));
    }

    private void DoAttack()
    {
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
        anim.SetTrigger("slashing");
        AudioSource.PlayClipAtPoint(attackSound, Camera.main.transform.position);
        GameObject go = Instantiate(attackPrefab, handPos.position, Quaternion.identity);
        go.transform.parent = handPos;
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
    }

    public void SetCanAttack(bool value)
    {
        canAttack = value;
    }
}
