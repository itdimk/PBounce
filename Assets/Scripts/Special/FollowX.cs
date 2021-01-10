using UnityEngine;

public class FollowX : MonoBehaviour
{
    public TargetProviderBaseX Target;
    public float Smoothness = 0.1f;
    public float MaxSpeed = 10f;
    private Vector3 _currVelocityRef;
    private Vector2 _currPhysicsVelocityRef;

    private Rigidbody2D _physics;
    private bool _usePhysics;

    // Start is called before the first frame update
    void Start()
    {
        _usePhysics = TryGetComponent(out _physics);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_usePhysics)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.GetTarget().position,
                ref _currVelocityRef, Smoothness, MaxSpeed);
        }
        else
        {
            Vector2 currVelocity = _physics.velocity;

            Vector2 targetVelocity = (Target.GetTarget().position - transform.position).normalized * MaxSpeed;

            _physics.velocity = Vector2.SmoothDamp(currVelocity,
                targetVelocity, ref _currPhysicsVelocityRef, Smoothness);
        }
    }
}