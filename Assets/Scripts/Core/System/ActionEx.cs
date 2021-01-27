using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEx
{
    private static Dictionary<Delegate, float> LastCallDict = new Dictionary<Delegate, float>();

    /// <summary>
    /// Returns true no more often then "cooldown" seconds
    /// </summary>
    /// <param name="key"> Delegate used as a key for cooldown calculation </param>
    /// <param name="cooldown"> The number of seconds to elapse since the last time THIS method returns true </param>
    /// <returns> True if cooldown is completed, otherwise False </returns>
    public static bool CheckCooldown(this Action key, float cooldown)
    {
        return CheckCooldown((Delegate) key, cooldown);
    }

    /// <summary>
    /// Returns true no more often then "cooldown" seconds
    /// </summary>
    /// <param name="key"> Delegate used as a key for cooldown calculation </param>
    /// <param name="cooldown"> The number of seconds to elapse since the last time THIS method returns true </param>
    /// <returns> True if cooldown is completed, otherwise False </returns>
    public static bool CheckCooldown(this Delegate key, float cooldown)
    {
        if (!LastCallDict.ContainsKey(key))
        {
            LastCallDict.Add(key, Time.time);
            return true;
        }

        if (Time.time - LastCallDict[key] >= cooldown)
        {
            LastCallDict[key] = Time.time;
            return true;
        }

        return false;
    }
}