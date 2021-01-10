using UnityEngine;
using System.Linq;

public class StaticForceField : MonoBehaviour
{
    public Vector2 Force = Vector2.up * 5;
    public bool DependsOnMass = false;
    public string[] IgnoredTags = { };


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
        {
            AddStaticForce(other.gameObject);
        }
    }

    private void AddStaticForce(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody2D physics))
        {
            if (DependsOnMass)
                physics.AddForce(Vector2.up * physics.mass * Force);
            else
                physics.AddForce(Vector2.up * Force);
        }
    }
}