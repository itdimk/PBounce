using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionOnOff : OnOff
{
    public enum Hooks
    {
        None,
        ColliderEnter,
        ColliderExit,
        ColliderExitExact,
    }
    
    public Hooks OnAt = Hooks.ColliderEnter;
    public Hooks OffAt;
    public string[] TargetColliderTags = {"Player"};
    
    private int insideTriggerCount;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (TargetColliderTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount++;
            SetAllByHook(Hooks.ColliderEnter);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (TargetColliderTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount--;
            SetAllByHook(Hooks.ColliderExitExact);
        }

        if (insideTriggerCount == 0)
            SetAllByHook(Hooks.ColliderExit);
    }


    private void SetAllByHook(Hooks hook)
    {
        if (OnAt == hook)
            TurnOn();
        else if (OffAt == hook)
            TurnOff();
    }
}
