using System;
using System.Reflection;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    public Component Output;
    public string OutputPropertyName;
    public float Multiplier = 1.0f;
    public float Offset = 0f;
    public string Prefix = "";
    public bool Round;

    private bool _initialized;
    private Action<float> _setPropertyValue;
    
    private void Initialize()
    {
        _initialized = true;
        var property = Output.GetType().GetProperty(OutputPropertyName);

        if (property != null)
            _setPropertyValue = CreateSetPropertyValueMethod(property);
        else
            throw new ArgumentException($"Property {OutputPropertyName} doesn't exist");
    }


    public void SetNumber(float number)
    {
        if(!_initialized)
            Initialize();
        
        number *= Multiplier;
        number += Offset;

        if (Round)
            number = Mathf.Round(number);

        _setPropertyValue(number);
    }

    private Action<float> CreateSetPropertyValueMethod(PropertyInfo propety)
    {
        if (propety.PropertyType == typeof(string))
        {
            var setValue = (Action<string>) propety.SetMethod.CreateDelegate(typeof(Action<string>), Output);
            return value => setValue(Prefix + value);
        }

        if (propety.PropertyType == typeof(float))
        {
            var setValue = (Action<float>) propety.SetMethod.CreateDelegate(typeof(Action<float>), Output);
            return setValue;
        }

        if (propety.PropertyType == typeof(int))
        {
            var setValue = (Action<int>) propety.SetMethod.CreateDelegate(typeof(Action<int>), Output);
            return value => setValue((int) value);
        }

        throw new ArgumentException($"Property type {propety.PropertyType} is not supported");
    }
}