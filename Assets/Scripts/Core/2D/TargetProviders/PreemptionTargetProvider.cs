using UnityEngine;

public class PreemptionTargetProvider : TargetProviderBase
{
    public TargetProviderBase Target;
    public float Scale = 0.04f;
    
    private GameObject PreemptionMarker;

    // Start is called before the first frame update
    void Start()
    {
        PreemptionMarker = new GameObject("PreemptionMarker");
    }

    public override Transform GetTarget()
    {
        var target = Target.GetTarget();

        if (target != null && target.gameObject.TryGetComponent(out Rigidbody2D physics))
        {
            Vector2 targetPos = target.transform.position;
            Vector2 myPos = transform.position;

            float distance = Vector2.Distance(targetPos, myPos);

            PreemptionMarker.transform.position = targetPos + physics.velocity * (distance * Scale);
            return PreemptionMarker.transform;
        }
        
        return PreemptionMarker.transform;
    }
}
