using System;
using UnityEngine;

public class RoundNumberPipe : BindingPipe
{
    public int Digits = 0;

    public override object Apply(object value)
    {
        if (value is float fvalue)
            return base.Apply(Math.Round(fvalue, Digits));

        if (value is double dvalue)
            return base.Apply(Math.Round(dvalue, Digits));

        Debug.LogWarning($"Unsupported type: {value.GetType()}");
        return base.Apply(value);
    }
}
