using UnityEngine;
using System.Linq;

public class StaticForceField : MonoBehaviour
{
    public Vector2 Force = Vector2.up * 5;
    public bool DependsOnMass = false;
    public bool DependsOnDensity = false;
    public string[] IgnoredTags = { };


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
        {
            AddStaticForce(other);
        }
    }

    private void AddStaticForce(Collider2D other)
    {
        if (other.TryGetComponent(out Rigidbody2D physics))
        {
            physics.AddForce(Vector2.up * Force *
                             (DependsOnMass ? physics.mass : 1) *
                             (DependsOnDensity ? (1.0F / other.density) : 1));
        }
    }
}