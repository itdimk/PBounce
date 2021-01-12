using System;
using System.Reflection;
using UnityEngine;

public abstract class PropertyDisplay<T> : Display<T>
{
    /// <summary>
    /// Becomes true at first "set value" call
    /// </summary>
    private bool _initialized;

    /// <summary>
    /// Delegate to set value of property directly (without reflection)
    /// </summary>
    protected Action<T> SetPropertyValue;

    [Tooltip("Component that used to show value (e.g. Text)")]
    public Component Output;

    [Tooltip("Property of this component used to show value (e.g. text)")]
    public string OutputPropertyName;



    /// <summary>
    /// Displays specified value in "Output" component
    /// </summary>
    public override void SetItemToDisplay(T value)
    {
        if (!_initialized)
            Initialize();

        SetPropertyValue(value);

        base.SetItemToDisplay(value);
    }

    /// <summary>
    /// Initializes delegate to set property directly
    /// </summary>
    private void Initialize()
    {
        _initialized = true;
        var property = Output.GetType().GetProperty(OutputPropertyName);

        if (property != null)
            SetPropertyValue = CreateSetPropertyValueMethod(property);
        else
            throw new ArgumentException($"Property {OutputPropertyName} doesn't exist");
    }

    /// <summary>
    /// Creates delegate to set property value directly
    /// </summary>
    /// <returns>That delegate</returns>
    protected virtual Action<T> CreateSetPropertyValueMethod(PropertyInfo property)
    {
        if (property.PropertyType == typeof(T))
        {
            var setValue = (Action<T>) property.SetMethod.CreateDelegate(typeof(Action<T>), Output);
            return setValue;
        }

        throw new ArgumentException($"Property type {property.PropertyType} is not supported");
    }
}