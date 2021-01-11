using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseInventoryDisplay : MonoBehaviour
{
    public BaseInventoryDisplay Next;

    public virtual void SetInventory(Inventory inventory)
    {
        if(Next != null)
            Next.SetInventory(inventory);
    }
}