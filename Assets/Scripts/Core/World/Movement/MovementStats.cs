using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementStats : MonoBehaviour
{
    public LayerMask WhatIsGround;

    [Space] public Rect GroundCheck = new Rect(-0.5f, -1f, 1, 1);
    [Space] public Rect GrabLedgeAirCheck = new Rect(0, 2, 5, 0.5f);
    [Space] public Rect GrabLedgeGroundCheck = new Rect(0, 1, 2.5f, 0.5f);

    [Space] public float GroundDetectionDistance = 100f;
    public float MaxCharacterTintToGrabLedge = 30f;
    public bool UseAbsoluteDirection;

    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public bool CanGrabLedge;
    [HideInInspector] public float DistanceToGround;
    [HideInInspector] public float AngleOfSurface;

    private Rigidbody2D _physics;

    void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        SetValuesByGroundRaycast();
        RefreshCanGrabLedge();
        RefreshIsGrounded();
    }

    private void RefreshIsGrounded()
    {
        var groundCheck = GroundCheck;
        groundCheck.position += (Vector2) transform.position;
        IsGrounded = CheckOverlap(groundCheck, WhatIsGround);
    }

    void RefreshCanGrabLedge()
    {
        if (Mathf.Abs(Mathf.DeltaAngle(_physics.rotation, 0)) > MaxCharacterTintToGrabLedge)
        {
            CanGrabLedge = false;
            return;
        }
        var airCheckRect = OffsetByTransform(GrabLedgeAirCheck);
        var groundCheckRect = OffsetByTransform(GrabLedgeGroundCheck);

        bool airCheck = CheckOverlap(airCheckRect, WhatIsGround);
        bool groundCheck = CheckOverlap(groundCheckRect, WhatIsGround);
        CanGrabLedge = !airCheck && groundCheck;
    }

    private void SetValuesByGroundRaycast()
    {
        var down = !UseAbsoluteDirection ? -transform.up : -Vector3.up;
        var hit = Physics2D.Raycast(_physics.position, down, GroundDetectionDistance, WhatIsGround);

        if (hit)
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


    private bool CheckOverlap(Rect area, LayerMask mask)
    {
        return Physics2D.OverlapArea(area.min, area.max, mask);
    }

    private void OnDrawGizmos()
    {
        DrawGroundCheck();
        DrawGrabLedgeChecks();
    }

    private void DrawGroundCheck()
    {
        var groundCheck = GroundCheck;
        groundCheck.position += (Vector2) transform.position;

        Gizmos.color = CheckOverlap(groundCheck, WhatIsGround) ? Color.magenta : Color.white;
        GizmosEx.DrawRect(groundCheck);
    }

    private void DrawGrabLedgeChecks()
    {
        var airCheck = OffsetByTransform(GrabLedgeAirCheck);
        var groundCheck = OffsetByTransform(GrabLedgeGroundCheck);


        Gizmos.color = !CheckOverlap(airCheck, WhatIsGround) ? Color.magenta : Color.white;
        GizmosEx.DrawRect(airCheck);

        Gizmos.color = CheckOverlap(groundCheck, WhatIsGround) ? Color.magenta : Color.white;
        GizmosEx.DrawRect(groundCheck);
    }

    private Rect OffsetByTransform(Rect target)
    {
        var pos = transform.position;
        var right = UseAbsoluteDirection ? 1 : Mathf.Sign(transform.right.x);

        if (right > 0)
            return new Rect(pos.x + target.x, pos.y + target.y, target.width, target.height);
        else
            return new Rect(pos.x - target.xMax, pos.y + target.y, target.width, target.height);
    }
}