using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StaticForce : MonoBehaviour
{
    private Rigidbody2D _physics;
    
    public Vector2 Force;
    
    // Start is called before the first frame update
    void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _physics.AddForce(Force);
    }
}
