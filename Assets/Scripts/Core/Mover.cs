using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody2D rig;
    SpriteRenderer rend;
    Animator anim;

    [SerializeField] bool canMove = true;
    [SerializeField] bool isGrounded = true;

    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Collider2D playerCollider;

    float horizontalInput;
    float verticalInput;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (!playerCollider)
        {
            playerCollider = GetComponent<Collider2D>();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            isGrounded = Physics2D.IsTouchingLayers(playerCollider, groundLayer);
            CheckInputs();
            ProcessInputs();
            ProcessAnimation();
        }
    }

    private void CheckInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void ProcessInputs()
    {
        float xVelocity = horizontalInput;
        float yVelocity = rig.velocity.y;

        if (isGrounded)
        {
            xVelocity = WalkAndRun(xVelocity);
            rig.velocity = (new Vector2(xVelocity, yVelocity));
            Jump();
        }
    }

    private void ProcessAnimation()
    {
        if (isGrounded) 
        {
            anim.SetBool("jumping", false);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("walking", true);
                if (CheckIfIsRunning())
                {
                    anim.SetBool("running", true);
                }
                else
                {
                    anim.SetBool("running", false);
                }
            }
            else
            {
                anim.SetBool("walking", false);
                anim.SetBool("running", false);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            anim.SetBool("walking", false);
        }
        FlipSprite();
    }

    private float WalkAndRun(float xVelocity)
    {
        if (CheckIfIsRunning())
        {
            xVelocity *= runSpeed;
        }
        else
        {
            xVelocity *= walkSpeed;
        }

        return xVelocity;
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            rig.velocity += jumpVelocity;
        }
    }

    private bool CheckIfIsRunning()
    {
        if (Input.GetKey(KeyCode.B))
        {
            Debug.Log("SEGURANDO B");
            return true;
        }
        else
        {
            Debug.Log("SOLTANDO B");
            return false;
        }
    }

    private void FlipSprite()
    {
        bool isMovingOnHorizontal = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        if (isMovingOnHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x),1f);
        }
    }

    public void CancelMovement()
    {
        canMove = false;
    }
}
