using UnityEngine;
using UnityEngine.Events;

public class FullInventoryDisplay : BaseInventoryDisplay
{
    public InventoryItemDisplay ItemDisplay;
    public WeaponInventoryItemDisplay WeaponItemDisplay;

    public UnityEvent<InventoryItem> ItemSelected;
    public bool TakeItemOnClick = true;

    private Inventory _inventory;

    private void Start()
    {
        ItemSelected.AddListener(OnItemSelected);
    }

    public override void SetInventory(Inventory inventory)
    {
        var items = inventory.AllItems;

        while (transform.childCount > 0)
            transform.GetChild(0).gameObject.PutToPool();


        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i] is WeaponInventoryItem weaponItem)
            {
                var prefab = WeaponItemDisplay.gameObject.GetCloneFromPool(transform);
                var component = prefab.GetComponent<WeaponInventoryItemDisplay>();

                component.Clicked.RemoveAllListeners();
                component.Clicked.AddListener(item => ItemSelected?.Invoke(item));
                weaponItem.OnShot.AddListener(() => component.SetItem(weaponItem));
                component.SetItem(weaponItem);
            }
            else
            {
                var prefab = ItemDisplay.gameObject.GetCloneFromPool(transform);
                var component = prefab.GetComponent<InventoryItemDisplay>();
                
                component.Clicked.RemoveAllListeners();
                component.Clicked.AddListener(item => ItemSelected?.Invoke(item));
                component.SetItem(items[i]);
              
            }
        }

        _inventory = inventory;
    }


    private void OnItemSelected(InventoryItem item)
    {
        if (TakeItemOnClick)
        {
            _inventory.TakeItem(item);
            gameObject.SetActive(false);
        }
    }
}