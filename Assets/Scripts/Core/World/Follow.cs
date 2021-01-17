using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public float Smoothness = 0.1f;
    public float MaxSpeed = 10f;
    public bool InstantMoving;
    
    private Vector3 _currVelocityRef;
    
    void FixedUpdate()
    {
        transform.position = InstantMoving
            ? Target.position
            : Vector3.SmoothDamp(transform.position, Target.position,
                ref _currVelocityRef, Smoothness, MaxSpeed);
    }
}