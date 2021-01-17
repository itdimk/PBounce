using System;
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
        // var target = Expression.Constant(TargetComponent);
        // var body = Expression.Call(target, property.GetGetMethod());
        //
        // return (Func<object>) Expression.Lambda(body).Compile();

        return () => property.GetValue(targetComponent);
    }

    public static Action<object> CreateSetMethod(PropertyInfo property, object targetComponent)
    {
        // var target = Expression.Constant(TargetComponent);
        // var param = Expression.Parameter(typeof(object));
        // var convertedParam = Expression.Convert(param, property.PropertyType);
        //
        // var body = Expression.Call(target, property.GetSetMethod(), convertedParam);
        //
        // return (Action<object>) Expression.Lambda(body, param).Compile();

        return (arg) => property.SetValue(targetComponent, Convert.ChangeType(arg, property.PropertyType));
    }
}
