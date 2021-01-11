using System;
using System.Reflection;
using UnityEngine;

public class StringDisplay : MonoBehaviour
{
    public Component Output;
    public string OutputPropertyName;

    private Action<string> _setPropertyValue;

    private bool _initialized;

    public StringDisplay Next;

    public void SetString(string value)
    {
        if(!_initialized)
            Initialize();
        
        _setPropertyValue(value);
        
        if(Next != null)
            Next.SetString(value);
    }

    private void Initialize()
    {
        _initialized = true;
        var property = Output.GetType().GetProperty(OutputPropertyName);

        if (property != null)
            _setPropertyValue = CreateSetPropertyValueMethod(property);
        else
            throw new ArgumentException($"Property {OutputPropertyName} doesn't exist");
    }

    private Action<string> CreateSetPropertyValueMethod(PropertyInfo propety)
    {
        if (propety.PropertyType == typeof(string))
        {
            var setValue = (Action<string>) propety.SetMethod.CreateDelegate(typeof(Action<string>), Output);
            return value => setValue(value.ToString());
        }

        throw new ArgumentException($"Property type {propety.PropertyType} is not supported");
    }
}
