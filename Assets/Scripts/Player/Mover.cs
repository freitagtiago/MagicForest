using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody2D _rigidBody2d;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private CapsuleCollider2D _playerCollider;
    [SerializeField] private BoxCollider2D _feetCollider;

    [SerializeField] private bool _canMove = true;

    [SerializeField] private float _verticalInput;
    [SerializeField] private float _horizontalInput;
    
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _jumpSped = 5f;

    [SerializeField] private AudioClip _audioJump;

    [Header("Ladder Movement Config")]
    [SerializeField] private float _climbSpeed = 1f;
    [SerializeField] private float _distanceFromMiddleCollider = 0.7f;
    [SerializeField] private float _topDistanceFromMiddleCollider = 0.3f;
    [SerializeField] private float _checkRadius = 0.3f;
    private bool _movingOnLadder = false;

    

    private void Awake()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _playerCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
    }
    
    private void FixedUpdate()
    {
        if (_canMove)
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
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void ProcessAnimation()
    {
        if (IsGrounded())
        {
            _animator.SetBool("jumping", false);
            if (Input.GetKey(KeyCode.RightArrow) 
                || Input.GetKey(KeyCode.LeftArrow))
            {
                _animator.SetBool("walking", true);
                if (CheckIfIsRunning())
                {
                    _animator.SetBool("running", true);
                }
                else
                {
                    _animator.SetBool("running", false);
                }
            }
            else
            {
                _animator.SetBool("walking", false);
                _animator.SetBool("running", false);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("walking", false);
                _animator.SetBool("running", false);
                _animator.SetBool("jumping", true);
            }
        }
        else
        {
            _animator.SetBool("walking", false);
        }
        FlipSprite();
    }

    private void WalkAndRun()
    {
        if(!IsGrounded() 
            || _movingOnLadder) 
        { 
            return; 
        }

        float xVelocity = Input.GetAxisRaw("Horizontal");
        float yVelocity = _rigidBody2d.velocity.y;

        if (CheckIfIsRunning())
        {
            xVelocity *= _runSpeed;
        }
        else
        {
            xVelocity *= _walkSpeed;
        }

        _rigidBody2d.velocity = (new Vector2(xVelocity, yVelocity));
    }

    private void Jump()
    {
        if (!IsGrounded()) 
        {
            if(_movingOnLadder)
            {
                return;
            }
            Vector2 jumpVelocity = new Vector2(_horizontalInput * _walkSpeed, _rigidBody2d.velocity.y);
            _rigidBody2d.velocity = jumpVelocity;

            return; 
        }

        if (Input.GetKey(KeyCode.Space))
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Vector2 jumpVelocity = new Vector2(horizontalInput, _jumpSped);
            _rigidBody2d.velocity = jumpVelocity;
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_audioJump);
            }
        }
    }

    private void ClimbLadder()
    {
        if (!IsOnLadder())
        {
            _animator.SetBool("climbing", false);
            _rigidBody2d.isKinematic = false;
            _movingOnLadder = false;
            return;
        }

        bool ladderTop = Physics2D.OverlapCircle(transform.position + new Vector3(0, _topDistanceFromMiddleCollider, 0)
                                                , _checkRadius
                                                , LayerMask.GetMask("Ladder"));
        bool ladderBottom = Physics2D.OverlapCircle(transform.position + new Vector3(0, -_distanceFromMiddleCollider, 0)
                                                    , _checkRadius
                                                    , LayerMask.GetMask("Ladder"));

        if(_verticalInput !=0)
        {
            _rigidBody2d.isKinematic = true;
            _movingOnLadder = true;
        }

        if (_movingOnLadder)
        {
            if (!ladderTop && _verticalInput > 0f)
            {
                FinishClimb();
                return;
            }

            if (!ladderBottom && _verticalInput < 0f)
            {
                FinishClimb();
                return;
            }

            _animator.SetFloat("yValue", _verticalInput);
            _animator.SetBool("jumping", false);
            _animator.SetBool("walking", false);
            _animator.SetBool("running", false);
            _animator.SetBool("climbing", true);

            Vector2 climbVelocity = new Vector2(0, _verticalInput * _climbSpeed);
            _rigidBody2d.velocity = climbVelocity;
        }
    }

    private void FinishClimb()
    {
        _movingOnLadder = false;
        _rigidBody2d.isKinematic = false;
        _animator.SetBool("climbing", false);
        _animator.SetFloat("yValue", 0);
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
        return _feetCollider.IsTouchingLayers(LayerMask.GetMask("Walkable"));
    }

    private bool IsOnLadder()
    {
        return _playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    private void FlipSprite()
    {
        bool isMovingOnHorizontal = Mathf.Abs(_rigidBody2d.velocity.x) > Mathf.Epsilon;
        if (isMovingOnHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody2d.velocity.x),1f);
        }
    }
    public void SetCanMove(bool value)
    {
        _canMove = value;
    }
    public void SpecialJump(float impulse)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 jumpVelocity = new Vector2(horizontalInput, impulse);
        _rigidBody2d.velocity = jumpVelocity;

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_audioJump);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, _topDistanceFromMiddleCollider, 0), _checkRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, -_distanceFromMiddleCollider, 0), _checkRadius);
    }
}
