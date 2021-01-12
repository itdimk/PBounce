using System.Linq;
using UnityEngine.Events;

public class QuickAccessInventoryDisplay : Display<Inventory>
{
    public FullInventoryDisplay FullInventoryDisplay;
    public InventoryItemDisplay[] ItemsDisplay;


    private void Start()
    {
        foreach (var item in ItemsDisplay)
            item.LongPress.AddListener(_ => SetupSlot(item));
    }

    public override void SetItemToDisplay(Inventory inventory)
    {
        var items = inventory.AllItems.Where(item => item.QuickAccess).Take(ItemsDisplay.Length).ToArray();

        for (int i = 0; i < items.Length; ++i)
        {
            var itemDisplay = ItemsDisplay[i];
            itemDisplay.Clicked.RemoveAllListeners();
            itemDisplay.Clicked.AddListener(inventory.TakeItem);

            itemDisplay.SetItemToDisplay(items[i]);
        }
        
        base.SetItemToDisplay(inventory);
    }

    private void SetupSlot(InventoryItemDisplay slot)
    {
        FullInventoryDisplay.TakeItemOnClick = false;
        FullInventoryDisplay.gameObject.SetActive(true);

        UnityAction<InventoryItem> listener = null;

        listener = item =>
        {
            slot.SetItemToDisplay(item);
         
            FullInventoryDisplay.gameObject.SetActive(false);
            FullInventoryDisplay.ItemSelected.RemoveListener(listener);
            FullInventoryDisplay.TakeItemOnClick = true;
        };

        FullInventoryDisplay.ItemSelected.AddListener(listener);
    }
}