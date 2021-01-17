using UnityEngine;

public class OneWayBinding
{
    public BindingParameter Source;
    public BindingParameter Destination;

    
    public void RefreshBinding()
    {
        var sourceValue = Source.GetValue();
        Destination.SetValue(sourceValue);
    }
}