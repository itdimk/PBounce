using System.Linq;
using UnityEngine;

public class TriggerSwitchX : SwitchX
{
    public enum HooksD
    {
        None,
        TriggerEnter,
        TriggerExit,
        TriggerExitExact,
    }

    public string[] TargetTriggerTags = {"Player"};

    public HooksD EnableOn;
    public HooksD DisableOn;

    private int insideTriggerCount;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetTriggerTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount++;
            SetAllByHook(HooksD.TriggerEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TargetTriggerTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount--;
            SetAllByHook(HooksD.TriggerExitExact);
        }

        if (insideTriggerCount == 0)
            SetAllByHook(HooksD.TriggerExit);
    }


    private void SetAllByHook(HooksD hook)
    {
        if (EnableOn == hook)
            EnableSwitch();
        else if (DisableOn == hook)
            DisableSwitch();
    }
}