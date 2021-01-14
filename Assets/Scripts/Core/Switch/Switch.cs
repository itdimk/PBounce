using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public UnityEvent Enabled = new UnityEvent();
    public UnityEvent Disabled = new UnityEvent();
    
    public bool IsEnabled;
    public bool SwitchIfRequiredOnly = true;

    public void EnableSwitch()
    {
        if (IsEnabled && SwitchIfRequiredOnly) return;

        Enabled.Invoke();
        IsEnabled = true;
    }

    public void DisableSwitch()
    {
        if (!IsEnabled && SwitchIfRequiredOnly) return;

        Disabled.Invoke();
        IsEnabled = false;
    }

    public void FlipSwitch()
    {
        if (IsEnabled)
            DisableSwitch();
        else
            EnableSwitch();
    }
}