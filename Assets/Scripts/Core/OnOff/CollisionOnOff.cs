using System.Linq;
using UnityEngine;

public class CollisionOnOff : OnOff
{
    public enum Hooks
    {
        None,
        ColliderEnter,
        ColliderExitAll,
        ColliderExitOne,
    }

    public Hooks OnAt = Hooks.ColliderEnter;
    public Hooks OffAt;
    public string[] TargetColliderTags = {"Player"};

    private int insideTriggerCount;

    private void OnCollisionEnter2D(Collision2D other) => HandleCollisionEnter(other.gameObject.tag);
    private void OnCollisionEnter(Collision other) => HandleCollisionEnter(other.gameObject.tag);

    private void HandleCollisionEnter(string targetTag)
    {
        if (TargetColliderTags.Contains(targetTag))
        {
            insideTriggerCount++;
            SetAllByHook(Hooks.ColliderEnter);
        }
    }

    private void OnCollisionExit2D(Collision2D other) => HandleCollisionExit(other.gameObject.tag);
    private void OnCollisionExit(Collision other) => HandleCollisionExit(other.gameObject.tag);

    private void HandleCollisionExit(string targetTag)
    {
        if (TargetColliderTags.Contains(targetTag))
        {
            insideTriggerCount--;
            SetAllByHook(Hooks.ColliderExitOne);
        }

        if (insideTriggerCount == 0)
            SetAllByHook(Hooks.ColliderExitAll);
    }


    private void SetAllByHook(Hooks hook)
    {
        if (OnAt == hook)
            TurnOn();
        else if (OffAt == hook)
            TurnOff();
    }
}