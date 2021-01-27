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

    [HideInInspector] public UnityEvent RefreshBindingRequired;

    private void Initialize()
    {
        _initialized = true;
        var componentType = TargetComponent.GetType();

        PropertyInfo prop = componentType.GetProperty(TargetProperty);
        FieldInfo field = componentType.GetField(TargetProperty);

        if (prop != null)
        {
            _setValue = ReflectionEx.CreateSetMethod(prop, TargetComponent);
            _getValue = ReflectionEx.CreateGetMethod(prop, TargetComponent);
        }
        else if (field != null)
        {
            _setValue = ReflectionEx.CreateSetMethod(field, TargetComponent);
            _getValue = ReflectionEx.CreateGetMethod(field, TargetComponent);
        }
        else
            Debug.LogError($"Field or Property \"{TargetProperty}\" doesn't exist in {TargetComponent}");
    }

    public void SetValue<T>(T value)
    {
        if (!_initialized)
            Initialize();

        _setValue(Pipe ? Pipe.Apply(value) : value);
    }


    public object GetValue()
    {
        if (!_initialized)
            Initialize();

        return Pipe ? Pipe.Apply(_getValue()) : _getValue();
    }


    public void RefreshBinding()
    {
        RefreshBindingRequired?.Invoke();
    }

    public override string ToString()
    {
        return TargetProperty;
    }
}