using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [HideInInspector] public float X;
    [HideInInspector] public float Y;

    protected virtual void FixedUpdate()
    {
        X = Input.GetAxisRaw("Horizontal");
        Y = Input.GetAxisRaw("Vertical");
    }
}