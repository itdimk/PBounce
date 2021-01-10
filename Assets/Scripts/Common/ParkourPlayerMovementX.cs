using System;
using UnityEngine;

public class ParkourPlayerMovementX : PlayerMovementX
{
    public bool DoubleJump;

    [Space] public Vector2 ClimbOffset1;
    public Vector2 ClimbOffset2;

    public float MaxTint = 180f;
    public float TintAirSmoothness = 0.5f;
    public float TintGroundSmoothness = 0.5f;

    private static readonly int AnimIsClimbing = Animator.StringToHash("IsClimbing");

    private float _currTintAirRef;
    private float _currTintGroundRef;
    private bool _doubleJumpUsed;
    private Animator _animator;

    private RigidbodyConstraints2D _constraints;

    private bool IsClimbing
    {
        get => _animator.GetBool(AnimIsClimbing);
        set => _animator.SetBool(AnimIsClimbing, value);
    }


    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>() ?? throw new ArgumentNullException(nameof(Animator));
        _constraints = Physics.constraints;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (MovementStats.IsGrounded)
            _doubleJumpUsed = false;

        if (MovementStats.CanGrabLedge && !IsClimbing && !MovementStats.IsGrounded)
            BeginClimb();

        TintPlayer();
    }


    protected override bool IsJumpRequired()
    {
        bool jump = InputY > 0.75f && DoubleJump && !_doubleJumpUsed;

        return base.IsJumpRequired() || jump;
    }

    protected override void Jump()
    {
        if (IsJumpRequired())
            ((Action)base.Jump).InvokeWithCooldown(JumpCooldown);

        if (!MovementStats.IsGrounded)
            _doubleJumpUsed = true;
    }

    private void BeginClimb()
    {
        IsClimbing = true;
        Physics.position += ClimbOffset1;
        Physics.constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled = false;
    }

    private void FinishClimb()
    {
        IsClimbing = false;
        Physics.constraints = _constraints;
        Physics.position +=
            new Vector2((ClimbOffset2.x + MovementStats.DistanceToLedge) * Mathf.Sign(transform.right.x),
                ClimbOffset2.y);
        this.enabled = true;
    }

    private void TintPlayer()
    {
        // FIX IT

        float angle;

        if (MovementStats.DistanceToGround > MovementStats.IsGroundedThreshold)
            angle = 0;
        else
            angle = Mathf.Clamp(MovementStats.AngleOfSurface - 90, -MaxTint, MaxTint);

        if (MovementStats.IsGrounded)
        {
            _currTintAirRef = 0f;
            Physics.rotation = Mathf.SmoothDampAngle(Physics.rotation, angle,
                ref _currTintGroundRef, TintGroundSmoothness);
        }
        else
        {
            _currTintGroundRef = 0f;
            Physics.rotation = Mathf.SmoothDampAngle(Physics.rotation, angle,
                ref _currTintAirRef, TintAirSmoothness);
        }
    }
}