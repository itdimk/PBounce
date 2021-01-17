using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEx
{
    private static Dictionary<Delegate, float> LastCallDict = new Dictionary<Delegate, float>();


    public static bool CheckCooldown(this Action key, float cooldown)
    {
        return CheckCooldown((Delegate) key, cooldown);
    }
    
    public static bool CheckCooldown(this Delegate key, float cooldown)
    {
        if (!LastCallDict.ContainsKey(key))
            LastCallDict.Add(key, 0);

        if (Time.time - LastCallDict[key] >= cooldown)
        {
            LastCallDict[key] = Time.time;
            return true;
        }

        return false;
    }
}