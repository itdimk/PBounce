using UnityEngine;

public class SmartMovementInput : MovementInput
{
    private bool _useDefaultInput = true;

    protected override void FixedUpdate()
    {
        if (_useDefaultInput)
            base.FixedUpdate();
    }

    public void SetX(float value)
    {
        _useDefaultInput = false;
        X = value;
    }

    public void SetY(float value)
    {
        _useDefaultInput = false;
        Y = value;
    }
}