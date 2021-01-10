using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class TimeSwitchX : SwitchX
{
    public float EnableInSeconds = -1f;
    public float DisableInSeconds = -1f;

    private float startTick;

    private bool isActivated;
    private bool isDeactivated;

    private void OnEnable()
    {
        startTick = Time.time;
    }

    private void FixedUpdate()
    {
        if (!isActivated && EnableInSeconds > 0 && Time.time >= startTick + EnableInSeconds)
        {
            isActivated = true;
            EnableSwitch();
        }

        if (!isDeactivated && DisableInSeconds > 0 && Time.time >= startTick + DisableInSeconds)
        {
            isDeactivated = true;
            DisableSwitch();
        }
    }
}