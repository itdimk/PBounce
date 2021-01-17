using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSlot : MonoBehaviour
{
    private InventoryItem _item;
    public bool IsEmpty => _item == null;
    public string[] Categories = new string[0];

    // Start is called before the first frame update
    void Start()
    {
        if (!IsEmpty)
            SetItem(_item);
    }


    public void SetItem(InventoryItem item)
    {
        if (!IsEmpty)
            Clear();

        this._item = item;
        item.gameObject.SetActive(true);
        _item.transform.rotation = transform.rotation;
        _item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;

    }

    public void Clear()
    {
        _item.gameObject.SetActive(false);
        _item.transform.parent = null;
        _item = null;
    }
}