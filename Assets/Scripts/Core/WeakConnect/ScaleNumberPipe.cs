using UnityEngine;

public class ScaleNumberPipe : BindingPipe
{
    public float Offset = 0F;
    public float Scale = 1.0f;

    public override object Apply(object value)
    {
        if (value is float fvalue)
            return base.Apply(fvalue * Scale + Offset);

        if (value is int ivalue)
            return base.Apply(ivalue * Scale + Offset);

        if (value is double dvalue)
            return base.Apply(dvalue * Scale + Offset);

        Debug.LogWarning($"Unsupported type: {value.GetType()}");
        return base.Apply(value);
    }
}