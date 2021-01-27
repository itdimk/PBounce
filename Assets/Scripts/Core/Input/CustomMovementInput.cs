using System;
using UnityEngine;

public class CustomMovementInput : MovementInput
{
    public bool UseDefaultInput = true;
    

    protected override void FixedUpdate()
    {
        // TODO: CRUTCH
        if (Application.platform == RuntimePlatform.WindowsEditor) UseDefaultInput = true;
        
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