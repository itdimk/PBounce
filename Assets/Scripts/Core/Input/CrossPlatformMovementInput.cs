using UnityEngine;

public class CrossPlatformMovementInput : MovementInput
{
    public bool UseDefaultInput = true;

    protected override void FixedUpdate()
    {
        if (!UseDefaultInput) return;

        base.FixedUpdate();
    }

    public void SetX(float value)
    {
        if(UseDefaultInput) return;
        X = value;
    }

    public void SetY(float value)
    {
        if(UseDefaultInput) return;
        Y = value;
    }
}