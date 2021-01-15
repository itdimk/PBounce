using System.Linq;
using UnityEngine;

public class GravitationalAnomaly : MonoBehaviour
{
    public float GravityForce = 500F;
    public float Fading = 0.5f;
    public string[] IgnoredTags = { };

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag) && other.TryGetComponent(out Rigidbody2D physics))
        {
            var direction = other.transform.position - transform.position;
            var distance = direction.magnitude;

            var fadedForce = Fading * distance;
            var fullForce = direction.normalized * GravityForce * Time.deltaTime;

            if (fadedForce > fullForce.magnitude) return;

            float percent = (fullForce.magnitude - fadedForce) / fullForce.magnitude;
            fullForce.Scale(new Vector3(percent, percent, percent));

            physics.AddForce(fullForce);
        }
    }
}