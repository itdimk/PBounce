using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public InventoryItem Item;

    private void Start()
    {
        if (TryGetComponent<InventoryItem>(out _))
            Debug.LogError("Inventory item can't be collectable item");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Inventory inventory))
        {
            Item.transform.parent = null;
            inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }
}