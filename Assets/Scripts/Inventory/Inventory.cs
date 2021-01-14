﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> DefaultItems = new List<InventoryItem>();
    [SerializeField] private ArmSlot[] ArmSlots = new ArmSlot[0];

    public IReadOnlyList<InventoryItem> Items => DefaultItems;
    
    public UnityEvent ItemsChanged;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    

    public void AddItem(InventoryItem item)
    {
        var existed = Items.FirstOrDefault(i => i.ID == item.ID);

        if (existed != null)
            existed.Count += item.Count;
        else
            DefaultItems.Add(item);
            
        item.transform.parent = transform;
        item.gameObject.SetActive(false);
        ItemsChanged?.Invoke();
    }

    public void TakeItem(InventoryItem item)
    {
        var slot = ArmSlots.FirstOrDefault(s => s.Categories.Contains(item.Category));

        if (slot != null)
            slot.SetItem(item);
        else
            Debug.Log($"Unable to find slot for {item.DisplayName}:{item.Category}");
    }
}