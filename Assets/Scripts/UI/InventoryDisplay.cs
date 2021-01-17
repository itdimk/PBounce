using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory TargetInventory;
    
    public InventoryItemDisplay ItemDisplay;
    public WeaponInventoryItemDisplay WeaponItemDisplay;
    
    public UnityEvent<InventoryItem> ItemSelected;
    public UnityEvent<InventoryItem> ItemLongPress;

    private void Start()
    {
        TargetInventory.ItemsChanged.AddListener(() => RefreshItems(TargetInventory.Items));
        RefreshItems(TargetInventory.Items);
    }

    private void RefreshItems(IReadOnlyList<InventoryItem> items)
    {
        while (transform.childCount > 0)
            transform.GetChild(0).gameObject.PutToPool();


        foreach (var item in items)
            if (item is WeaponInventoryItem weaponItem)
            {
                var displayObject = WeaponItemDisplay.gameObject.GetCloneFromPool(transform);
                var display = displayObject.GetComponent<WeaponInventoryItemDisplay>();

                SetListenersToItemDisplay(display);
                weaponItem.OnShot.AddListener(() => display.SetItem(weaponItem));
                display.SetItem(weaponItem);
            }
            else
            {
                var displayObject = ItemDisplay.gameObject.GetCloneFromPool(transform);
                var display = displayObject.GetComponent<InventoryItemDisplay>();

                SetListenersToItemDisplay(display);
                display.SetItemID(item.ID);
            }
    }

    private void SetListenersToItemDisplay(InventoryItemDisplay display)
    {
        display.Clicked.RemoveAllListeners();
        display.Clicked.AddListener(OnItemSelected);
        display.LongPress.AddListener(OnItemLongPress);
    }


    private void OnItemSelected(InventoryItem item)
    {
        ItemSelected?.Invoke(item);
    }

    private void OnItemLongPress(InventoryItem item)
    {
        ItemLongPress?.Invoke(item);
    }
}