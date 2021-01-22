using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStatsX))]
public class SmoothJump : MonoBehaviour
{
    private MovementStatsX _stats;
    private Rigidbody2D _physics;

    private float _inputY;
    private bool _isJumping;
    private int _currJumpForceIndex;

    public int[] JumpForces = {150, 100, 75};
    public float AddForceInterval = 0.04f;
    public float JumpCooldown = 0.4f;
    [Space] public float MaxFallingSpeedToJump = 6;
    [Range(0, 1.0f)] public float FallingSpeedCompensation;

    private void Start()
    {
        _stats = GetComponent<MovementStatsX>();
        _physics = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _inputY = Input.GetAxisRaw("Vertical");

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
        if (_stats.IsGrounded && ActionEx.CheckCooldown(ResetForceIfRequired, JumpCooldown))
            _currJumpForceIndex = 0;
    }

    private void RefreshIsJumping()
    {
        if (_inputY > 0.1f && _stats.IsGrounded)
            _isJumping = true;

        if (_inputY < 0.1f)
            _isJumping = false;
    }
}