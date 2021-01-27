using System;
using System.Linq.Expressions;
using System.Reflection;

public static class ReflectionEx
{
    public static Func<object> CreateGetMethod(FieldInfo field, object targetComponent)
    {
        return () => field.GetValue(targetComponent);
    }

    public static Action<object> CreateSetMethod(FieldInfo field, object targetComponent)
    {
        return (arg) => field.SetValue(targetComponent, Convert.ChangeType(arg, field.FieldType));
    }

    public static Func<object> CreateGetMethod(PropertyInfo property, object targetComponent)
    {
        if (property.GetMethod == null) return null;
        
        var target = Expression.Constant(targetComponent);
        var body = Expression.Call(target, property.GetMethod);
        var convert = Expression.Convert(body, typeof(object));
        return (Func<object>) Expression.Lambda(convert).Compile();
    }

    public static Action<object> CreateSetMethod(PropertyInfo property, object targetComponent)
    {
        if (property.SetMethod == null) return null;
        
        var converter = new Func<object, Type, object>(Convert.ChangeType);
        
        var type = Expression.Constant(property.PropertyType);
        var target = Expression.Constant(targetComponent);
        var param = Expression.Parameter(typeof(object));
        
        var convert1 = Expression.Call( converter.Method, param, type);
        var convert2 = Expression.Convert(convert1, property.PropertyType);
        var body = Expression.Call(target, property.SetMethod, convert2);
        
        var result = (Action<object>) Expression.Lambda(body, param).Compile();
        return result;
    }
}