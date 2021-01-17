using UnityEngine;
using System.Linq;

public class StaticForceField : MonoBehaviour
{
    public Vector2 Force = Vector2.up * 5;
    public bool ScaleByMass = false;
    public string[] IgnoredTags = { };

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
            AddStaticForce(other);
    }

    private void AddStaticForce(Collider2D other)
    {
        if (other.TryGetComponent(out Rigidbody2D physics))
            physics.AddForce(Force * (ScaleByMass ? physics.mass : 1) * Time.deltaTime);
    }
}