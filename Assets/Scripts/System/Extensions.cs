using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static Dictionary<Delegate, float> LastCallDict = new Dictionary<Delegate, float>();

    public static void InvokeWithCooldown(this Action action, float cooldown)
    {
        if (!LastCallDict.ContainsKey(action))
            LastCallDict.Add(action, 0);

        if (Time.time - LastCallDict[action] >= cooldown)
        {
            action.Invoke();
            LastCallDict[action] = Time.time;
        }
    }
    
    public static void InvokeWithCooldown<T>(this Action<T> action, T arg, float cooldown)
    {
        if (!LastCallDict.ContainsKey(action))
            LastCallDict.Add(action, 0);

        if (Time.time - LastCallDict[action] >= cooldown)
        {
            action.Invoke(arg);
            LastCallDict[action] = Time.time;
        }
    }
}