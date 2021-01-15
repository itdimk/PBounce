using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnOff : OnOff
{
    public string[] Buttons = new string[0];

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var b in Buttons)
        {
            if (Input.GetButtonDown(b))
                base.TurnOn();

            if (Input.GetButtonUp(b))
                TurnOff();
        }
    }
}