using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Layer Masks")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Movement")]
    [SerializeField] private float _moveAcceleration = 40.0f;
    [SerializeField] private float _maxMoveSpeed = 12.0f;
    [SerializeField] private float _groundLinearDrag = 7.0f;
    private float _horizontalDirection;
    private bool _changingDirection => (_rb.velocity.x > 0f && _horizontalDirection < 0f) || (_rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 12.0f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpFallMultiplier = 2.5f;

    [Header("Ground Collision")]
    [SerializeField] private float _groundRaycastLength;
    [SerializeField] private float _groundRaycastOffsetLeft = -0.09f;
    [SerializeField] private float _groundRaycastOffsetRight = 0.13f;
    [SerializeField] private bool _isOnGround;

    private bool _canJump => Input.GetButton("Jump") && _isOnGround;

    [Header("Animations")]
    private Animator _animator;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        _horizontalDirection = GetInput().x;
        FlipSprite();
        AnimateRunIdle();
        if (_canJump)
        {
            Jump();
        }
        AnimateJump();
    }

    private void FlipSprite()
    {
        if (_horizontalDirection < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_horizontalDirection > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    private void AnimateRunIdle()
    {
        _animator.SetFloat("HorizontalDirection", Mathf.Abs(_horizontalDirection));
    }

    private void AnimateJump()
    {
        _animator.SetBool("IsJumping", !_isOnGround);
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();

        if (_isOnGround)
        {
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            ApplyFallMultiplier();
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0) * _moveAcceleration);

        if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
        }
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _groundLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void ApplyFallMultiplier()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    private void CheckCollisions()
    {
        bool leftOnGround = Physics2D.Raycast(
            new Vector3(
                   transform.position.x + _groundRaycastOffsetRight,
                   transform.position.y,
                   transform.position.z
            ),
            Vector2.down,
            _groundRaycastLength,
            _groundLayer
        );

        bool rightOnGround = Physics2D.Raycast(
             new Vector3(
                   transform.position.x + _groundRaycastOffsetLeft,
                   transform.position.y,
                   transform.position.z
            ),
            Vector2.down,
            _groundRaycastLength,
            _groundLayer
        );

        _isOnGround = leftOnGround || rightOnGround;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // Draw Raycasts
        Gizmos.DrawLine(
            new Vector3(
                   transform.position.x + _groundRaycastOffsetRight,
                   transform.position.y,
                   transform.position.z
            ),
            new Vector3(
                   transform.position.x + _groundRaycastOffsetRight,
                   transform.position.y - _groundRaycastLength,
                   transform.position.z
            )
        );

        Gizmos.DrawLine(
            new Vector3(
                   transform.position.x + _groundRaycastOffsetLeft,
                   transform.position.y,
                   transform.position.z
            ),
            new Vector3(
                   transform.position.x + _groundRaycastOffsetLeft,
                   transform.position.y - _groundRaycastLength,
                   transform.position.z
            )
        );

    }

}
