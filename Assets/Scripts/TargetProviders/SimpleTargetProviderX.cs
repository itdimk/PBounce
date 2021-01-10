using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTargetProviderX : TargetProviderBaseX
{
    public Transform Target;
    
    void Start()
    {
        
    }

    public override Transform GetTarget()
    {
        return Target;
    }
}
