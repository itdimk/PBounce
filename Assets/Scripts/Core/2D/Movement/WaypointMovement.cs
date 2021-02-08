using System;
using System.Collections.Generic;
using UnityEngine;


public class WaypointMovement : MonoBehaviour
{
    public enum EndReachedActions
    {
        DoNothing,
        GoToFirst,
        GoReverse
    }

    public List<Transform> waypoints = new List<Transform>();

    public float Speed = 5;
    public float Smoothness = 0.1f;
    public bool UseSmoothness = true;

    [Space] public float NextWaypointDistance = 1F;
    public EndReachedActions EndReachedAction = EndReachedActions.GoReverse;

    private int _currWaypointIndex;
    private Vector3 _currVelocity;

    private Rigidbody2D _physics;

    void Start()
    {
        if (!TryGetComponent(out _physics))
            Debug.LogError($"Can't get {nameof(Rigidbody2D)} component of {gameObject}");
    }

    private void OnDisable()
    {
        if (_physics)
            _physics.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        SetNextWpIfRequired();
        MoveToCurrentWp();
    }

    public void AddSpeed(float offset)
    {
        Speed += offset;
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

        bool isWpReached = IsReached(pos, nextWpPos);
        bool isEndReached = isWpReached && IsLastWaypoint(_currWaypointIndex);

        if (isEndReached)
            switch (EndReachedAction)
            {
                case EndReachedActions.DoNothing:
                    return;
                case EndReachedActions.GoToFirst:
                    _currWaypointIndex = 0;
                    return;
                case EndReachedActions.GoReverse:
                    waypoints.Reverse();
                    _currWaypointIndex = 0;
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        if (isWpReached)
            _currWaypointIndex = (_currWaypointIndex + 1) % waypoints.Count;
    }

    private bool IsLastWaypoint(int index) => index >= waypoints.Count - 1;

    private bool IsReached(Vector2 pos, Vector2 waypoint)
    {
        return Vector2.Distance(pos, waypoint) <= NextWaypointDistance;
    }
}