using UnityEngine;

public class SmartMovementInput : MovementInput
{
    private bool _useDefaultInput = true;

    protected override void Update()
    {
        if (_useDefaultInput)
            base.Update();
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