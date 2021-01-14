using UnityEngine;

public abstract class BindingPipe : MonoBehaviour
{
    public BindingPipe Next;

    public virtual object Apply(object value)
    {
        if (Next != null)
            return Next.Apply(value);
        return value;
    }
}