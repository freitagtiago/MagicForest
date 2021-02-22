using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody2D rig;
    SpriteRenderer rend;
    Animator anim;
    float defaultGravity;

    [SerializeField] bool canMove = true;

    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSped = 5f;
    [SerializeField] float climbSpeed = 5f;

    [SerializeField] CapsuleCollider2D playerCollider;
    [SerializeField] BoxCollider2D feetCollider;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        defaultGravity = rig.gravityScale;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            WalkAndRun();
            Jump();
            ClimbLadder();
            ProcessAnimation();
        }
    }

    private void ProcessAnimation()
    {
        if (IsGrounded()) 
        {
            anim.SetBool("climbing", false);
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
            anim.SetBool("climbing", false);
        }
        if (IsOnLadder())
        {
            bool isMovingVertical = Mathf.Abs(rig.velocity.y) > Mathf.Epsilon;
            anim.SetFloat("yValue", Input.GetAxisRaw("Vertical"));
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetBool("jumping", false);
            anim.SetBool("climbing", true);
        }

        FlipSprite();
    }

    private void WalkAndRun()
    {
        if(!IsGrounded()) { return; }

        float xVelocity = Input.GetAxisRaw("Horizontal");
        float yVelocity = rig.velocity.y;

        if (CheckIfIsRunning())
        {
            xVelocity *= runSpeed;
        }
        else
        {
            xVelocity *= walkSpeed;
        }

        rig.velocity = (new Vector2(xVelocity, yVelocity));
    }

    private void Jump()
    {
        if (!IsGrounded()) { return; }

        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSped);
            rig.velocity += jumpVelocity;
        }
    }

    private void ClimbLadder()
    {
        if(!IsOnLadder()) 
        {
            rig.gravityScale = defaultGravity;
            rig.isKinematic = false;
            return; 
        }

        rig.gravityScale = 0;
        rig.isKinematic = true;
        float yVelocity = Input.GetAxisRaw("Vertical");
        float xVelocity = Input.GetAxisRaw("Horizontal");
        Vector2 climbVelocity = new Vector2(xVelocity, yVelocity * climbSpeed);
        rig.velocity = climbVelocity;
    }

    private bool CheckIfIsRunning()
    {
        if (Input.GetKey(KeyCode.B))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsGrounded()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Walkable"));
    }

    private bool IsOnLadder()
    {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    private void FlipSprite()
    {
        bool isMovingOnHorizontal = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        if (isMovingOnHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x),1f);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
