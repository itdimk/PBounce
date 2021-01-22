using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStatsX))]
public class PlayerMovementX : MonoBehaviour
{
    private bool _isFacingRight;
    private Vector2 _currVelocityRef;

    protected float InputX;
    protected Rigidbody2D Physics;
    protected MovementStatsX MovementStats;
    
    Vector2 Right => UseAbsoluteDirection ? Vector3.right : transform.right;
    
    [Space] public float Speed = 10f;
    [Range(0.0f, 1.0f)] public float Smoothness = 0.2f;
    [Range(0.0f, 1.0f)] public float AirControl = 0.5f;

    [Space] public bool UseAbsoluteDirection;
    public bool UseFlip;


    protected virtual void Start()
    {
        _isFacingRight = transform.right.x > 0;
        Physics = GetComponent<Rigidbody2D>();
        MovementStats = GetComponent<MovementStatsX>();
    }
    
    protected void FixedUpdate()
    {
        SetInputX();
        Move();
    }

    protected virtual void Move()
    {
        Vector2 currVelocity = Physics.velocity;
        Vector2 targetVelocity = GetTargetVelocity();

        Physics.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref _currVelocityRef, Smoothness);

        FlipIfRequired();
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
    
    private Vector2 GetTargetVelocity()
    {
        float speedX = Mathf.Abs(InputX) * (MovementStats.IsGrounded ? Speed : Speed * AirControl);

        Vector2 targetVelocity = UseFlip ? speedX * Right : Mathf.Sign(InputX) * speedX * Right;
        targetVelocity.y = Physics.velocity.y;

        return targetVelocity;
    }
    
    protected virtual void SetInputX()
    {
        InputX = Input.GetAxis("Horizontal");
    }
}
