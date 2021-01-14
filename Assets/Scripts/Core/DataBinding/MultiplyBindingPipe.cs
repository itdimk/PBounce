using UnityEngine;

public class MultiplyBindingPipe : BindingPipe
{
    public float Scale = 1.0f;
    
    public override object Apply(object value)
    {
        if (value is float fvalue)
            return base.Apply(fvalue * Scale);

        if (value is int ivalue)
            return base.Apply(ivalue * Scale);
        
        Debug.LogWarning($"Unsupported type: {value.GetType()}");
        return base.Apply(value);
    }
}