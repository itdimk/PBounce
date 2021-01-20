using System;
using UnityEngine;

public class MovementStatsX : MonoBehaviour
{
    public LayerMask WhatIsGround;
    public float GroundDetectionDistance = 10f;
    public Rect GroundCheck = new Rect(-0.5f, -1f, 1, 1);


    public float MaxCharacterTintToGrabLedge = 30f;
    public float GrabLedgeGroundDistance = 2f;
    public float GrabLedgeAirDistance = 5f;
    public Vector2 GrabLedgeAirCheckOffset = new Vector2(-0.5f, 4f);
    public Vector2 GrabLedgeGroundCheckOffset = new Vector2(-0.5f, 1f);
    public bool UseAbsoluteDirection;
    
    [HideInInspector] public bool CanGrabLedge;
    [HideInInspector] public bool IsGrounded;

    [HideInInspector] public float DistanceToLedge;
    [HideInInspector] public float DistanceToGround;
    [HideInInspector] public float AngleOfSurface;


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
        SetValuesByLedgeRaycasts();
        RefreshIsGrounded();
    }


    private void RefreshIsGrounded()
    {
        var groundCheck = GroundCheck;
        groundCheck.position += (Vector2) transform.position;

        var col = Physics2D.OverlapArea(groundCheck.min, groundCheck.max, WhatIsGround);
        IsGrounded = col != default;
    }
    void SetValuesByGroundRaycast()
    {
        var tfm = transform;
        var origin = _physics.position;
        
        var down = !UseAbsoluteDirection ? -tfm.up : -Vector3.up;
        var hit = Physics2D.Raycast(origin, down, GroundDetectionDistance, WhatIsGround);
        
        if (hit != default)
        {
            DistanceToGround = hit.distance;
            AngleOfSurface = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
        }
        else
        {
            DistanceToGround = float.PositiveInfinity;
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

    private void OnDrawGizmos()
    {
        DrawIsGroundCheck(Color.white);

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
    
    private void DrawIsGroundCheck(Color color)
    {
        Gizmos.color = color;
        var check = GroundCheck;
        check.position += (Vector2) transform.position;
        
        Gizmos.DrawLine(check.min, new Vector2(check.xMin, check.yMax));
        Gizmos.DrawLine(check.min, new Vector2(check.xMax, check.yMin));
        Gizmos.DrawLine(check.max, new Vector2(check.xMax, check.yMin));
        Gizmos.DrawLine(check.max, new Vector2(check.xMin, check.yMax));
    }
}