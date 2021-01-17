using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodOnOff : OnOff
{
    public float OnEverySecond = -1;
    public float OffEverySecond = -1;
    
    // Update is called once per frame
    void Update()
    {
        if (ActionEx.CheckCooldown(TurnOn, OnEverySecond))
            TurnOn();
        
        if (ActionEx.CheckCooldown(TurnOff, OffEverySecond))
            TurnOff();
    }
}
