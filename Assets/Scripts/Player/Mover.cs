using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody2D rig;
    SpriteRenderer rend;
    Animator anim;

    [SerializeField] bool canMove = true;

    [SerializeField] float verticalInput;
    [SerializeField] float horizontalInput;
    
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSped = 5f;

    [SerializeField] AudioClip audioJump;

    [Header("Ladder Movement Config")]
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] float distanceFromMiddleCollider = 0.7f;
    [SerializeField] float topDistanceFromMiddleCollider = 0.3f;
    [SerializeField] float checkRadius = 0.3f;
    bool movingOnLadder = false;

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

    private void FixedUpdate()
    {
        if (canMove)
        {
            GetInputs();
            WalkAndRun();
            Jump();
            ClimbLadder();
            ProcessAnimation();
        }
    }

    private void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void ProcessAnimation()
    {
        if (IsGrounded())
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
                anim.SetBool("walking", false);
                anim.SetBool("running", false);
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            anim.SetBool("walking", false);
        }
        FlipSprite();
    }

    private void WalkAndRun()
    {
        if(!IsGrounded() || movingOnLadder) { return; }

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
        if (!IsGrounded()) 
        {
            if(movingOnLadder){
                return;
            }
            Vector2 jumpVelocity = new Vector2(horizontalInput * walkSpeed, rig.velocity.y);
            rig.velocity = jumpVelocity;

            return; 
        }

        if (Input.GetKey(KeyCode.Space))
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Vector2 jumpVelocity = new Vector2(horizontalInput, jumpSped);
            rig.velocity += jumpVelocity;
            AudioSource.PlayClipAtPoint(audioJump, Camera.main.transform.position);
        }
    }

    private void ClimbLadder()
    {
        if (!IsOnLadder())
        {
            anim.SetBool("climbing", false);
            rig.isKinematic = false;
            movingOnLadder = false;
            return;
        }

        bool ladderTop = Physics2D.OverlapCircle(transform.position + new Vector3(0, topDistanceFromMiddleCollider, 0), checkRadius, LayerMask.GetMask("Ladder"));
        bool ladderBottom = Physics2D.OverlapCircle(transform.position + new Vector3(0, -distanceFromMiddleCollider, 0), checkRadius, LayerMask.GetMask("Ladder"));

        if(verticalInput !=0)
        {
            rig.isKinematic = true;
            movingOnLadder = true;
        }

        if (movingOnLadder)
        {
            if (!ladderTop && verticalInput > 0f)
            {
                FinishClimb();
                return;
            }

            if (!ladderBottom && verticalInput < 0f)
            {
                FinishClimb();
                return;
            }

            anim.SetFloat("yValue", verticalInput);
            anim.SetBool("jumping", false);
            anim.SetBool("climbing", true);

            Vector2 climbVelocity = new Vector2(0, verticalInput * climbSpeed);
            rig.velocity = climbVelocity;
        }
    }

    private void FinishClimb()
    {
        movingOnLadder = false;
        rig.isKinematic = false;
        anim.SetBool("climbing", false);
        anim.SetFloat("yValue", 0);
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






    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, topDistanceFromMiddleCollider, 0), checkRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, -distanceFromMiddleCollider, 0), checkRadius);
    }
}
