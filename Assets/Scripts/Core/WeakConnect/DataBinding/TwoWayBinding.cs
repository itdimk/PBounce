using System;
using UnityEngine;

public class TwoWayBinding 
{
    public BindingParameter Source;
    public BindingParameter Destination;

    private IComparable _lastSourceValue;
    private IComparable _lastDestinationValue;
    
    public void RefreshBinding()
    {
        var sourceValue = Source.GetValue();
        var destinationValue = Destination.GetValue();

        if (sourceValue is IComparable srcComp && destinationValue is IComparable dstComp)
        {
            if (srcComp.CompareTo(_lastSourceValue) != 0)
            {
                Destination.SetValue(sourceValue);
                _lastSourceValue = srcComp;
                _lastDestinationValue = srcComp;
            }
            else if (dstComp.CompareTo(_lastDestinationValue) != 0)
            {
                Source.SetValue(destinationValue);
                _lastSourceValue = dstComp;
                _lastDestinationValue = dstComp;
            }
        }
        else
            Debug.LogError($"{sourceValue.GetType()} and {destinationValue.GetType()}" +
                           $"  must implement {nameof(IComparable)} interface");
    }
}