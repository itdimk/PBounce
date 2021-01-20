using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var angles = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(angles.x, angles.y, angles.z +  Speed * Time.deltaTime);
    }
}