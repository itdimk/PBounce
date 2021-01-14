using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class TimeSwitch : Switch
{
    public float EnableInSeconds = -1f;
    public float DisableInSeconds = -1f;

    private float startTick;
    private void OnEnable()
    {
        startTick = Time.time;
    }

    private void FixedUpdate()
    {
        if (EnableInSeconds > 0 && Time.time >= startTick + EnableInSeconds)
            EnableSwitch();

        if (DisableInSeconds > 0 && Time.time >= startTick + DisableInSeconds)
            DisableSwitch();
    }
}