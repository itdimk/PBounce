using System.Linq;
using UnityEngine;

public class TriggerOnOff : OnOff
{
    public enum Hooks
    {
        None,
        TriggerEnter,
        TriggerExit,
        TriggerExitExact,
    }
    
    public Hooks OnAt = Hooks.TriggerEnter;
    public Hooks OffAt;
    public string[] TargetTriggerTags = {"Player"};
    
    private int insideTriggerCount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetTriggerTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount++;
            SetAllByHook(Hooks.TriggerEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TargetTriggerTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount--;
            SetAllByHook(Hooks.TriggerExitExact);
        }

        if (insideTriggerCount == 0)
            SetAllByHook(Hooks.TriggerExit);
    }


    private void SetAllByHook(Hooks hook)
    {
        if (OnAt == hook)
            TurnOn();
        else if (OffAt == hook)
            TurnOff();
    }
}