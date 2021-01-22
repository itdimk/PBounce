using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementStatsX : MonoBehaviour
{
    public Rect GroundCheck = new Rect(-0.5f, -1f, 1, 1);
    public Rect GrabLedgeAirCheck = new Rect(0, 2, 5, 0.5f);
    public Rect GrabLedgeGroundCheck = new Rect(0, 1, 2.5f, 0.5f);

    public LayerMask WhatIsGround;
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

        bool airCheck = CheckOverlap(GrabLedgeAirCheck, WhatIsGround);
        bool groundCheck = CheckOverlap(GrabLedgeGroundCheck, WhatIsGround);
        CanGrabLedge = !airCheck && groundCheck;
    }

    private void SetValuesByGroundRaycast()
    {
        var down = !UseAbsoluteDirection ? -transform.up : -Vector3.up;
        var hit = Physics2D.Raycast(_physics.position, down, GroundDetectionDistance, WhatIsGround);

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


    private bool CheckOverlap(Rect area, LayerMask mask)
    {
        return Physics2D.OverlapArea(area.min, area.max, WhatIsGround) != default;
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
        var airCheck = GrabLedgeAirCheck;
        var groundCheck = GrabLedgeGroundCheck;

        airCheck.position +=(Vector2) transform.position;
        groundCheck.position += (Vector2) transform.position;

        Gizmos.color = !CheckOverlap(airCheck, WhatIsGround) ? Color.magenta : Color.white;
        GizmosEx.DrawRect(airCheck);
        
        Gizmos.color = CheckOverlap(groundCheck, WhatIsGround) ? Color.magenta : Color.white;
        GizmosEx.DrawRect(groundCheck);
    }
   
}