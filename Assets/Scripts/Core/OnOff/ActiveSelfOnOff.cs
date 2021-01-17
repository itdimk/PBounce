using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelfOnOff : OnOff
{
    void OnEnable()
    {
        base.TurnOn();
    }

    private void OnDisable()
    {
        base.TurnOff();
    }
}
