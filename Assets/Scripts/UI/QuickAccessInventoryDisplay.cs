using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QuickAccessInventoryDisplay : MonoBehaviour
{
    public Inventory TargetInventory;

    public InventoryDisplay FullInventoryDisplay;
    public InventoryItemDisplay[] ItemDisplays;


    private void Start()
    {
        foreach (var item in ItemDisplays)
        {
            item.LongPress.AddListener(_ => SetupSlot(item));
            item.Clicked.AddListener(OnDisplayClick);
        }

        RefreshItems(TargetInventory.Items);
    }

    private void RefreshItems(IReadOnlyList<InventoryItem> items)
    {
        items = items.Where(item => item.QuickAccess).Take(ItemDisplays.Length).ToArray();

        for (int i = 0; i < items.Count; ++i)
            ItemDisplays[i].SetItem(items[i]);
    }

    private void SetupSlot(InventoryItemDisplay slot)
    {
        FullInventoryDisplay.gameObject.SetActive(true);

        void Listener(InventoryItem item)
        {
            slot.SetItem(item);

            FullInventoryDisplay.gameObject.SetActive(false);
            FullInventoryDisplay.ItemSelected.RemoveListener(Listener);
        }

        FullInventoryDisplay.ItemSelected.AddListener(Listener);
    }

    private void OnDisplayClick(InventoryItem item)
    {
        if (item != null)
            TargetInventory.TakeItem(item);
    }
}