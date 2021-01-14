using System;
using UnityEngine;

public class MovementStatsX : MonoBehaviour
{
    public LayerMask WhatIsGround;
    public float GroundDetectionDistance = 10f;
    public float GroundDetectionWidth = 2f;
    public float CeilingDetectionDistance = 10f;

    public float IsGroundedThreshold = 2f;
    public float IsCrawlingThreshold = 2f;

    public float MaxCharacterTintToGrabLedge = 30f;
    public float GrabLedgeGroundDistance = 2f;
    public float GrabLedgeAirDistance = 5f;
    public Vector2 GrabLedgeAirCheckOffset = new Vector2(-0.5f, 4f);
    public Vector2 GrabLedgeGroundCheckOffset = new Vector2(-0.5f, 1f);
    public bool SetAnimatorParameters;

    [HideInInspector] public bool CanGrabLedge;
    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public bool IsCrawling;

    [HideInInspector] public float DistanceToLedge;
    [HideInInspector] public float DistanceToGround;
    [HideInInspector] public float DistanceToCeiling;
    [HideInInspector] public float AngleOfSurface;

    private static readonly int
        AnimSpeedH = Animator.StringToHash("SpeedH"),
        AnimSpeedV = Animator.StringToHash("SpeedV"),
        AnimSpeedX = Animator.StringToHash("SpeedX"),
        AnimSpeedY = Animator.StringToHash("SpeedY"),
        AnimGrounded = Animator.StringToHash("IsGrounded");

    private Rigidbody2D _physics;
    private Animator _animator;

    void Start()
    {
        _physics = GetComponent<Rigidbody2D>() ?? throw new ArgumentNullException(nameof(Rigidbody2D));
        _animator = GetComponent<Animator>() ?? throw new ArgumentNullException(nameof(Animator));
    }

    void FixedUpdate()
    {
        SetValuesByGroundRaycast();
        SetValuesByCeilingRaycast();
        SetValuesByLedgeRaycasts();
        SetAnimatorParams();
    }

    void SetValuesByCeilingRaycast()
    {
        var hit = Physics2D.Raycast(_physics.position, transform.up, GroundDetectionDistance, WhatIsGround);

        if (hit != default)
        {
            DistanceToCeiling = hit.distance;
            IsCrawling = DistanceToCeiling <= IsCrawlingThreshold;
        }
        else
        {
            DistanceToCeiling = float.PositiveInfinity;
            IsCrawling = false;
        }
    }

    void SetValuesByGroundRaycast()
    {
        var tfm = transform;
        var origin1 = _physics.position + (Vector2) tfm.right * GroundDetectionWidth;
        var origin2 = _physics.position - (Vector2) tfm.right * GroundDetectionWidth;

        var hit1 = Physics2D.Raycast(origin1, -tfm.up, GroundDetectionDistance, WhatIsGround);
        var hit2 = Physics2D.Raycast(origin2, -tfm.up, GroundDetectionDistance, WhatIsGround);

        var hit = hit1 != default ? hit1 : hit2;

        if (hit != default)
        {
            DistanceToGround = hit.distance;
            IsGrounded = DistanceToGround <= IsGroundedThreshold;
            AngleOfSurface = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
        }
        else
        {
            DistanceToGround = float.PositiveInfinity;
            IsGrounded = false;
            AngleOfSurface = 90;
        }
    }

    void SetValuesByLedgeRaycasts()
    {
        if (Mathf.Abs(Mathf.DeltaAngle(_physics.rotation, 0)) > MaxCharacterTintToGrabLedge)
        {
            CanGrabLedge = false;
            return;
        }

        Vector2 right = new Vector2(transform.right.x, 0);
        Vector2 position = _physics.position;

        Vector2 groundCheckOffset = new Vector2(GrabLedgeGroundCheckOffset.x * right.x, GrabLedgeGroundCheckOffset.y);
        Vector2 airCheckOffset = new Vector2(GrabLedgeAirCheckOffset.x * right.x, GrabLedgeAirCheckOffset.y);

        Vector2 groundRayOrigin = position + groundCheckOffset;
        Vector2 airRayOrigin = position + airCheckOffset;


        var groundHit = Physics2D.Raycast(groundRayOrigin, right, GrabLedgeGroundDistance, WhatIsGround);
        var airHit = Physics2D.Raycast(airRayOrigin, right, GrabLedgeAirDistance, WhatIsGround);

        CanGrabLedge = groundHit != default && airHit == default;

        DistanceToLedge = groundHit != default
            ? Vector2.Distance(groundHit.point, _physics.position)
            : float.PositiveInfinity;
    }

    void SetAnimatorParams()
    {
        if (!SetAnimatorParameters) return;

        _animator.SetFloat(AnimSpeedX, _physics.velocity.x);
        _animator.SetFloat(AnimSpeedY, _physics.velocity.y);
        _animator.SetBool(AnimGrounded, IsGrounded);
        _animator.SetFloat(AnimSpeedH, Mathf.Abs(_physics.velocity.x));
        _animator.SetFloat(AnimSpeedV, Mathf.Abs(_physics.velocity.y));
    }

    private void OnDrawGizmos()
    {
        // Draw ground raycast

        var tfm = transform;
        var origin1 = transform.position + tfm.right * GroundDetectionWidth;
        var origin2 = transform.position - tfm.right * GroundDetectionWidth;

        var hit1 = Physics2D.Raycast(origin1, -tfm.up, IsGroundedThreshold, WhatIsGround);
        var hit2 = Physics2D.Raycast(origin2, -tfm.up, IsGroundedThreshold, WhatIsGround);

        Gizmos.color = hit1 != default ? Color.red : Color.cyan;
        Gizmos.DrawLine(origin1, origin1 - transform.up * IsGroundedThreshold);

        Gizmos.color = hit2 != default ? Color.red : Color.cyan;
        Gizmos.DrawLine(origin2, origin2 - transform.up * IsGroundedThreshold);

        // Graw grab ledge raycasts

        if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, 0)) <= MaxCharacterTintToGrabLedge)
        {
            Vector2 right = new Vector2(transform.right.x, 0);
            Vector2 position = transform.position;

            Vector2 groundCheckOffset =
                new Vector2(GrabLedgeGroundCheckOffset.x * right.x, GrabLedgeGroundCheckOffset.y);
            Vector2 airCheckOffset = new Vector2(GrabLedgeAirCheckOffset.x * right.x, GrabLedgeAirCheckOffset.y);

            Vector2 groundRayOrigin = position + groundCheckOffset;
            Vector2 airRayOrigin = position + airCheckOffset;

            var groundHit = Physics2D.Raycast(groundRayOrigin, right, GrabLedgeGroundDistance, WhatIsGround);
            var airHit = Physics2D.Raycast(airRayOrigin, right, GrabLedgeAirDistance, WhatIsGround);

            Gizmos.color = groundHit != default ? Color.red : Color.cyan;
            Gizmos.DrawLine(groundRayOrigin, groundRayOrigin + right * GrabLedgeGroundDistance);

            Gizmos.color = airHit == default ? Color.red : Color.cyan;
            Gizmos.DrawLine(airRayOrigin, airRayOrigin + right * GrabLedgeAirDistance);
        }
    }
}