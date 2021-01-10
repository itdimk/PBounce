using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float Force;
    public float Fading = 0.5f;
    public string[] IgnoredTags = { };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
        {
            var direction = other.transform.position - transform.position;
            var distance = direction.magnitude;
            var force = direction.normalized * (1.0f  / (distance * Fading)) * Force;

            if (other.TryGetComponent(out Rigidbody2D physics))
                physics.AddForce(force);
        }
    }
}