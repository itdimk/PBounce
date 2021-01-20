using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStatsX))]
public class PlayerMovementX : MonoBehaviour
{
    private Vector2 _currVelocityRef;
    private bool _isFacingRight;

    protected Rigidbody2D Physics;
    protected MovementStatsX MovementStats;

    protected float InputX;
    protected float InputY;

    public float JumpForce = 1000f;
    public float JumpCooldown = 0.25f;
    public float MaxFallingSpeedToJump = 1f;

    [Space] public float Speed = 10f;
    [Range(0.0f, 1.0f)] public float Smoothness = 0.2f;
    [Range(0.0f, 1.0f)] public float AirControl = 0.5f;

    [Space] public bool UseAbsoluteDirection;
    public bool UseFlip = false;


    protected virtual void Start()
    {
        _isFacingRight = transform.right.x > 0;
        Physics = GetComponent<Rigidbody2D>();
        MovementStats = GetComponent<MovementStatsX>();
    }

    protected virtual void Update()
    {
        SetInputX();
        SetInputY();

        if (IsJumpRequired() && ActionEx.CheckCooldown(Jump, JumpCooldown))
            Jump();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector2 currVelocity = Physics.velocity;
        Vector2 targetVelocity = GetTargetVelocity();

        Physics.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref _currVelocityRef, Smoothness);

        FlipIfRequired();
    }

    protected Vector2 GetRight() => UseAbsoluteDirection ? Vector3.right : transform.right;


    protected virtual bool IsJumpRequired()
    {
        return MovementStats.IsGrounded && Physics.velocity.y >= -MaxFallingSpeedToJump;
    }

    protected virtual void Jump()
    {
        Physics.AddForce(JumpForce * InputY * Vector2.up);
    }

    private void FlipIfRequired()
    {
        if (!UseFlip) return;

        if (InputX > 0 && !_isFacingRight || InputX < 0 && _isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
            _isFacingRight = !_isFacingRight;
        }
    }

    protected virtual void SetInputX()
    {
        InputX = Input.GetAxis("Horizontal");
    }

    protected virtual void SetInputY()
    {
        InputY = JumpInput.GetJumpInput(0.7f);
    }

    private Vector2 GetTargetVelocity()
    {
        float speedX = Mathf.Abs(InputX) * (MovementStats.IsGrounded ? Speed : Speed * AirControl);

        Vector2 targetVelocity = UseFlip ? speedX * GetRight() : Mathf.Sign(InputX) * speedX * GetRight();
        targetVelocity.y = Physics.velocity.y;

        return targetVelocity;
    }
}