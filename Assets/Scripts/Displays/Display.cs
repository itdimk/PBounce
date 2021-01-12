using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display<T> : MonoBehaviour
{
    [Tooltip("If not null, value will be shown in next display too")]
    public Display<T> Next;

    public virtual void SetItemToDisplay(T item)
    {
        if (Next != null)
            Next.SetItemToDisplay(item);
    }
}