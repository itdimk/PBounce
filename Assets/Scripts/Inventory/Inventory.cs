using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> Items = new List<InventoryItem>();
    [SerializeField] private ArmSlot[] ArmSlots = new ArmSlot[0];

    public IReadOnlyList<InventoryItem> AllItems => Items;
    public BaseInventoryDisplay[] Output = { };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void AddItem(InventoryItem item)
    {
        Items.Add(item);
        item.transform.parent = transform;
        item.gameObject.SetActive(false);

        foreach (var o in Output)
            o.SetInventory(this);
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