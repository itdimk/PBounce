using UnityEngine;
using UnityEngine.Events;

public class OnOff : MonoBehaviour
{
    public UnityEvent On;
    public UnityEvent Off;

    [HideInInspector] public bool IsEnabled;
    public bool DoNotEnableTwice = true;

    public void TurnOn()
    {
        if (IsEnabled && DoNotEnableTwice) return;

        On.Invoke();
        IsEnabled = true;
    }

    public void TurnOff()
    {
        if (!IsEnabled && DoNotEnableTwice) return;

        Off.Invoke();
        IsEnabled = false;
    }

    public void Flip()
    {
        if (IsEnabled)
            TurnOff();
        else
            TurnOn();
    }
}