using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponentInjection : MonoBehaviour
{
    public string SourceGameObjectName;
    public Component TargetComponent;
    public string TargetProperty;

    private Type _propertyType;
    private Action<object> _setValue;
    private bool _initialized;

    private void Awake()
    {
        GameObject source = GameObject.Find(SourceGameObjectName);

        if(source != null)
            Inject(source);
        else
            Debug.LogError($"Can't find injection source: \"{SourceGameObjectName}\" for {gameObject.name}");
    }


    private void Inject(GameObject value)
    {
        if (!_initialized)
            Initialize();

        if (_propertyType.IsInstanceOfType(value))
            _setValue(value);
        else
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