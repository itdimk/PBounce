using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToStringPipe : BindingPipe
{
    public override object Apply(object value)
    {
        var timeSpan = new TimeSpan(0, 0, 0, 0, (int) (Time.time * 1000));
        return base.Apply($"{timeSpan.Minutes} : {timeSpan.Seconds}");
    }
}