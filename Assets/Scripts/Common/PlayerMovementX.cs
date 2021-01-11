using System;
using TreeEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovementStatsX))]
public class PlayerMovementX : MonoBehaviour
{
    protected Rigidbody2D Physics;
    protected MovementStatsX MovementStats;

    protected float InputX;
    protected float InputY;
    protected bool IsFacingRight = false;
    protected bool UseJoystick;

    private Vector2 _currVelocityRef;


    public Joystick Stick;


    [Space] public float JumpForce = 1000f;
    public float JumpCooldown = 0.25f;
    public float MaxFallingSpeedToJump = 1f;


    [Space] public float Speed = 10f;
    [Range(0.0f, 1.0f)] public float Smoothness = 0.2f;
    public float AirControl = 0.5f;

    public bool DisableFlip = false;

    protected virtual void Start()
    {
        IsFacingRight = transform.right.x > 0;
        UseJoystick = Stick != null;
        Physics = GetComponent<Rigidbody2D>() ?? throw new NullReferenceException("No Rigidbody2D");
        MovementStats = GetComponent<MovementStatsX>();
    }

    protected virtual void Update()
    {
        SetInputX();
        SetInputY();
    }

    protected virtual void FixedUpdate()
    {
        Move();

        if (IsJumpRequired())
        {
            ((Action) Jump).InvokeWithCooldown(JumpCooldown);
        }
    }

    protected virtual void Move()
    {
        Vector2 currVelocity = Physics.velocity;

        float speedX = Mathf.Abs(InputX) * (MovementStats.IsGrounded ? Speed : Speed * AirControl);

        var right = (!DisableFlip ? 1 : Mathf.Sign(InputX)) * transform.right;

        Vector2 targetVelocity = right * speedX;

        targetVelocity.y = currVelocity.y;

        Physics.velocity = Vector2.SmoothDamp(currVelocity,
            targetVelocity, ref _currVelocityRef, Smoothness);

        FlipIfRequired();
    }

    protected virtual bool IsJumpRequired()
    {
        return InputY > 0.75f && MovementStats.IsGrounded && Physics.velocity.y >= -MaxFallingSpeedToJump;
    }

    protected virtual void Jump()
    {
        Physics.AddForce(Vector2.up * JumpForce);
    }

    private void FlipIfRequired()
    {
        if (DisableFlip) return;

        if (InputX > 0 && !IsFacingRight || InputX < 0 && IsFacingRight)
        {
            transform.Rotate(0, 180f, 0);
            IsFacingRight = !IsFacingRight;
        }
    }

    protected virtual void SetInputX()
    {
        InputX = !UseJoystick
            ? Input.GetAxisRaw("Horizontal")
            : Mathf.Clamp(Input.GetAxisRaw("Horizontal") + Stick.Horizontal, -1f, 1f);
    }

    protected virtual void SetInputY()
    {
        InputY = !UseJoystick
            ? Input.GetAxisRaw("Vertical")
            : Mathf.Clamp(Input.GetAxisRaw("Vertical") + Stick.Vertical, -1f, 1f);

        if (Input.GetButton("Jump"))
            InputY = 1f;
    }
}