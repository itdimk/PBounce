using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToStringPipe : BindingPipe
{
    public override object Apply(object value)
    {
        float fvalue = (float) Convert.ChangeType(value, typeof(float));
        var timeSpan = new TimeSpan(0, 0, 0, 0, (int) (fvalue * 1000));
        return base.Apply(
            $"{timeSpan.Minutes:00}.{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}"); 
    }
}