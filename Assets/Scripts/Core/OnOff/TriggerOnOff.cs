using System.Linq;
using UnityEngine;

public class TriggerOnOff : OnOff
{
    public enum Hooks
    {
        None,
        TriggerEnter,
        TriggerExitAll,
        TriggerExitOne,
    }

    public Hooks OnAt = Hooks.TriggerEnter;
    public Hooks OffAt;
    public string[] TargetTriggerTags = {"Player"};

    private int insideTriggerCount;


    private void OnTriggerEnter2D(Collider2D other) => HandleTriggerEnter(other.gameObject.tag);
    private void OnTriggerEnter(Collider other) => HandleTriggerEnter(other.gameObject.tag);

    private void HandleTriggerEnter(string targetTag)
    {
        if (TargetTriggerTags.Contains(targetTag))
        {
            insideTriggerCount++;
            SetAllByHook(Hooks.TriggerEnter);
        }
    }

    private void OnTriggerExit(Collider other) => HandleTriggerExit(other.gameObject.tag);
    private void OnTriggerExit2D(Collider2D other) => HandleTriggerExit(other.gameObject.tag);

    private void HandleTriggerExit(string targetTag)
    {
        if (TargetTriggerTags.Contains(targetTag))
        {
            insideTriggerCount--;
            SetAllByHook(Hooks.TriggerExitOne);
        }

        if (insideTriggerCount == 0)
            SetAllByHook(Hooks.TriggerExitAll);
    }


    private void SetAllByHook(Hooks hook)
    {
        if (OnAt == hook)
            TurnOn();
        else if (OffAt == hook)
            TurnOff();
    }
}