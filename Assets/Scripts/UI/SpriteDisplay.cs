using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpriteDisplay : MonoBehaviour
{
    public Component Output;
    public string OutputPropertyName;

    private Action<Sprite> _setPropertyValue;

    private bool _initialized;


    public void SetSprite(Sprite value)
    {
        if(!_initialized)
            Initialize();
        
        _setPropertyValue(value);
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

    private Action<Sprite> CreateSetPropertyValueMethod(PropertyInfo propety)
    {
        if (propety.PropertyType == typeof(Sprite))
        {
            var setValue = (Action<Sprite>) propety.SetMethod.CreateDelegate(typeof(Action<Sprite>), Output);
            return setValue;
        }

        throw new ArgumentException($"Property type {propety.PropertyType} is not supported");
    }
}
