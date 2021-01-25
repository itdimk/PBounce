using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStats))]
public class SmoothJump : MonoBehaviour
{
    private MovementStats _stats;
    private Rigidbody2D _physics;

    private bool _preventDoubleJump;
    private bool _isJumping;
    private int _currJumpForceIndex;

    public MovementInput Input;
    public float AddForceInterval = 0.04f;
    public float JumpCooldown = 0.4f;
    public float PreventDoubleJumpCooldown = 2.0f;
    public int[] JumpForces = {150, 100, 75};

    [Space] public float MaxFallingSpeedToJump = 6;
    [Range(0, 1.0f)] public float FallingSpeedCompensation;

    private void Start()
    {
        _stats = GetComponent<MovementStats>();
        _physics = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RefreshPreventHighJump();
        RefreshIsJumping();
        ResetForceIfRequired();
        JumpIfRequired();
    }

    private void JumpIfRequired()
    {
        if (_physics.velocity.y < -MaxFallingSpeedToJump) return;
        if (!_isJumping || _currJumpForceIndex >= JumpForces.Length) return;

        if (ActionEx.CheckCooldown(JumpIfRequired, AddForceInterval))
            _physics.AddForce(GetNextJumpForce());
    }


    private Vector2 GetNextJumpForce()
    {
        float amount = JumpForces[_currJumpForceIndex++];

        if (_physics.velocity.y < 0)
            amount *= 1 - _physics.velocity.y * FallingSpeedCompensation;

        return new Vector2(0, amount);
    }

    private void ResetForceIfRequired()
    {
        if (!_preventDoubleJump && _stats.IsGrounded && ActionEx.CheckCooldown(ResetForceIfRequired, JumpCooldown))
        {
            _currJumpForceIndex = 0;
            _preventDoubleJump = true;
        }
    }

    private void RefreshPreventHighJump()
    {
        if (!_stats.IsGrounded | ActionEx.CheckCooldown(ResetForceIfRequired, PreventDoubleJumpCooldown))
            _preventDoubleJump = false;
    }

    private void RefreshIsJumping()
    {
        if (Input.Y > 0.1f && _stats.IsGrounded )
            _isJumping = true;

        if (Input.Y < 0.1f)
            _isJumping = false;
    }
}