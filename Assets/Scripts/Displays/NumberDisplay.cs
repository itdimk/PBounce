using System;
using System.Reflection;
using UnityEngine;

public class NumberDisplay : PropertyDisplay<float>
{
    public float Multiplier = 1.0f;
    public float Offset = 0f;
    public bool Round;


    public override void SetItemToDisplay(float number)
    {
        base.SetItemToDisplay(number);

        float num = number * Multiplier + Offset;

        if (Round)
            num = Mathf.Round(num);

        SetPropertyValue(num);
    }

    protected override Action<float> CreateSetPropertyValueMethod(PropertyInfo property)
    {
        if (property.PropertyType == typeof(string))
        {
            var setValue = (Action<string>) property.SetMethod.CreateDelegate(typeof(Action<string>), Output);
            return value => setValue(value.ToString());
        }

        if (property.PropertyType == typeof(float))
        {
            var setValue = (Action<float>) property.SetMethod.CreateDelegate(typeof(Action<float>), Output);
            return setValue;
        }

        if (property.PropertyType == typeof(int))
        {
            var setValue = (Action<int>) property.SetMethod.CreateDelegate(typeof(Action<int>), Output);
            return value => setValue((int) value);
        }

        throw new ArgumentException($"Property type {property.PropertyType} is not supported");
    }
}