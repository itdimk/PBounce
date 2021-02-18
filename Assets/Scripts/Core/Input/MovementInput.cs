using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [HideInInspector] public float X;
    [HideInInspector] public float Y;

    protected virtual void Update()
    {
        X = Input.GetAxisRaw("Horizontal");
        Y = Input.GetAxisRaw("Vertical");
    }
}