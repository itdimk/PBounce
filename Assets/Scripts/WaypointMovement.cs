using System.Collections.Generic;
using UnityEngine;


public class WaypointMovement : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    public float Speed = 5;
    public float IsReachedDistance = 0.1F;
    public float Smoothness = 0.1f;
    public bool UseSmoothness = true;
    public bool Cycled = true;

    private bool _isReached = false;
    private int _currWaypointIndex;
    private Vector3 _currVelocity;

    private Rigidbody2D _physics;

    void Start()
    {
        if (!TryGetComponent(out _physics))
            Debug.LogError($"Can't get {nameof(Rigidbody2D)} component of {gameObject}");
    }

    private void FixedUpdate()
    {
        SetNextWpIfRequired();
        MoveToCurrentWp();
    }

    private void MoveToCurrentWp()
    {
        var pos = transform.position;
        var nextWpPos = waypoints[_currWaypointIndex].position;
        nextWpPos.z = pos.z = 0;

        var velocity = (nextWpPos - pos).normalized * Speed;

        if (UseSmoothness)
            _physics.velocity = Vector3.SmoothDamp(_physics.velocity, velocity,
                ref _currVelocity, Smoothness, Speed);
        else
            _physics.velocity = velocity;

        SetNextWpIfRequired();
    }

    private void SetNextWpIfRequired()
    {
        var pos = transform.position;
        var nextWpPos = waypoints[_currWaypointIndex].position;

        if (IsReached(pos, nextWpPos))
        {
            if (!Cycled && _currWaypointIndex >= waypoints.Count - 1) return;
            _currWaypointIndex = (_currWaypointIndex + 1) % waypoints.Count;
        }
    }

    private bool IsReached(Vector2 pos, Vector2 waypoint)
    {
        return Vector2.Distance(pos, waypoint) <= IsReachedDistance;
    }
}