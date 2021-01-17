using System;
using System.Reflection;
using UnityEngine;

public class ComponentInjectionTarget : MonoBehaviour
{
    public string InjectionID;
    public Component TargetComponent;
    public string TargetProperty;

    private Type _propertyType;
    private Action<object> _setValue;
    private bool _initialized;
    
    public void Inject(GameObject value)
    {
        if(!_initialized)
            Initialize();

        _setValue(value.GetComponent(_propertyType));
    }

    private void Initialize()
    {
        _initialized = true;
        var componentType = TargetComponent.GetType();

        PropertyInfo prop = componentType.GetProperty(TargetProperty);
        FieldInfo field = componentType.GetField(TargetProperty);

        if (prop != null)
        {
            _setValue = ReflectionEx.CreateSetMethod(prop, TargetComponent);
            _propertyType = prop.PropertyType;
        }
        else if (field != null)
        {
            _setValue = ReflectionEx.CreateSetMethod(field, TargetComponent);
            _propertyType = field.FieldType;
        }
        else
            Debug.LogError($"Field or Property \"{TargetProperty}\" doesn't exist in {TargetComponent}");
    }
}
