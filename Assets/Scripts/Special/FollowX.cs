using UnityEngine;

public class FollowX : MonoBehaviour
{
    public TargetProviderBaseX Target;
    public float Smoothness = 0.1f;
    public float MaxSpeed = 10f;
    public bool InstantMoving;


    private Vector3 _currVelocityRef;
    private Vector2 _currPhysicsVelocityRef;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = InstantMoving
            ? Target.GetTarget().position
            : Vector3.SmoothDamp(transform.position, Target.GetTarget().position,
                ref _currVelocityRef, Smoothness, MaxSpeed);
    }
}