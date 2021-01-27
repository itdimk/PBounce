using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStats))]
public class SmoothJump : MonoBehaviour
{
    private MovementStats _stats;
    private Rigidbody2D _physics;

    private bool _isJumping;
    private int _currJumpForceIndex;

    public MovementInput Input;
    public float AddForceInterval = 0.04f;
    public float JumpCooldown = 0.4f;
    public int[] JumpForces = {150, 100, 75};

    public UnityEvent Jump;
    
    private void Start()
    {
        _stats = GetComponent<MovementStats>();
        _physics = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RefreshIsJumping();
        ResetForceIfRequired();
        JumpIfRequired();
    }

    private void JumpIfRequired()
    {
        if (!_isJumping || _currJumpForceIndex >= JumpForces.Length) return;

        if (ActionEx.CheckCooldown(JumpIfRequired, AddForceInterval))
            _physics.AddForce(GetNextJumpForce());
    }


    private Vector2 GetNextJumpForce()
    {
        float amount = JumpForces[_currJumpForceIndex++];

        return new Vector2(0, amount);
    }

    private void ResetForceIfRequired()
    {
        if (_currJumpForceIndex == 0 || !_stats.IsGrounded) return;

        bool allForcesApplied = _currJumpForceIndex >= JumpForces.Length;
        bool shouldResetForce = !_isJumping || _isJumping && allForcesApplied;

        if (shouldResetForce && ActionEx.CheckCooldown(ResetForceIfRequired, JumpCooldown))
            _currJumpForceIndex = 0;
    }

    private void RefreshIsJumping()
    {
        if (Input.Y > 0.1f && _stats.IsGrounded && !_isJumping)
        {
            _isJumping = true;
            Jump?.Invoke();
        }

        if (Input.Y < 0.1f)
            _isJumping = false;
    }
}