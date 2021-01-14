using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class BindingParameter : MonoBehaviour
{
    public enum ParameterType
    {
        OneWaySource,
        OneWayDestination,
        TwoWaySource,
        TwoWayDestination
    }

    public string BindingID;
    public ParameterType Type;
    public Component TargetComponent;
    public string TargetProperty;
    public BindingPipe Pipe;

    private Func<object> _getValue;
    private Action<object> _setValue;

    private bool _initialized;

    [HideInInspector] public UnityEvent ValueChanged;

    private void Initialize()
    {
        var componentType = TargetComponent.GetType();

        PropertyInfo prop = componentType.GetProperty(TargetProperty);
        FieldInfo field = componentType.GetField(TargetProperty);

        if (prop != null)
        {
            _setValue = CreateSetMethod(prop);
            _getValue = CreateGetMethod(prop);
        }
        else if (field != null)
        {
            _setValue = CreateSetMethod(field);
            _getValue = CreateGetMethod(field);
        }
        else
            Debug.LogError($"Field or Property \"{TargetProperty}\" doesn't exist in {TargetComponent}");
    }

    public void SetValue<T>(T value)
    {
        if (!_initialized)
            Initialize();

        _setValue(Pipe != null ? Pipe.Apply(value) : value);
    }

    public object GetValue()
    {
        if (!_initialized)
            Initialize();

        return _getValue();
    }

    private Func<object> CreateGetMethod(FieldInfo field)
    {
        return () => field.GetValue(TargetComponent);
    }

    private Action<object> CreateSetMethod(FieldInfo field)
    {
        return (arg) => field.SetValue(TargetComponent, Convert.ChangeType(arg, field.FieldType));
    }

    private Func<object> CreateGetMethod(PropertyInfo property)
    {
        // var target = Expression.Constant(TargetComponent);
        // var body = Expression.Call(target, property.GetGetMethod());
        //
        // return (Func<object>) Expression.Lambda(body).Compile();

        return () => property.GetValue(TargetComponent);
    }

    Action<object> CreateSetMethod(PropertyInfo property)
    {
        // var target = Expression.Constant(TargetComponent);
        // var param = Expression.Parameter(typeof(object));
        // var convertedParam = Expression.Convert(param, property.PropertyType);
        //
        // var body = Expression.Call(target, property.GetSetMethod(), convertedParam);
        //
        // return (Action<object>) Expression.Lambda(body, param).Compile();

        return (arg) => property.SetValue(TargetComponent, Convert.ChangeType(arg, property.PropertyType));
    }

    public void OnValueChanged()
    {
        ValueChanged?.Invoke();
    }

    public override string ToString()
    {
        return TargetProperty;
    }
}