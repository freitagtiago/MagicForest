using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    Rigidbody2D rig;
    [SerializeField] CircleCollider2D groundCollider;
    [SerializeField] float moveSpeed = 1f;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (IsFacingRight())
        {
            Move(1);
        }
        else
        {
            Move(-1);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    private void Move(float direction)
    {
        rig.velocity = new Vector2(moveSpeed * direction, 0f) ;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rig.velocity.x)), 1f);
    }
}
