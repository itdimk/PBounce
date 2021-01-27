using UnityEngine;

public class SimpleTargetProvider : TargetProviderBase
{
    public Transform Target;
    

    public override Transform GetTarget()
    {
        return Target;
    }
}
