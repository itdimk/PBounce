using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    public SpriteDisplay Icon;
    public StringDisplay ItemName;
    public StringDisplay ItemDescription;
    public NumberDisplay ItemCount;
    public Button Button;

    [HideInInspector] public UnityEvent<InventoryItem> Clicked;
    [HideInInspector]  public UnityEvent<InventoryItem> LongPress;

    private InventoryItem _item;

    // Start is called before the first frame update
    void OnEnable()
    {
        Button.onClick.RemoveAllListeners();
        
        Button.onClick.AddListener(() => Clicked?.Invoke(_item));
        Button.gameObject.AddComponent<LongPressTrigger>()
            .OnLongPress.AddListener(() => LongPress?.Invoke(_item));
    }

    public void SetItem(InventoryItem item)
    {
        if (item != null)
        {
            SetItemIcon(item.Icon);
            SetItemName(item.DisplayName);
            SetItemDescription(item.Description);
            SetItemCount(item.Count);
        }
        else
        {
            SetItemIcon(null);
            SetItemName(null);
            SetItemDescription(null);
            SetItemCount(0);
        }

        _item = item;
    }

    private void SetItemName(string value)
    {
        if (ItemName != null)
            ItemName.SetString(!string.IsNullOrEmpty(value) ? value : "No item");
    }

    private void SetItemDescription(string value)
    {
        if (ItemDescription != null)
            ItemDescription.SetString(!string.IsNullOrEmpty(name) ? value : "No description");
    }

    private void SetItemCount(int count)
    {
        if (ItemCount != null)
            ItemCount.SetNumber(count);
    }

    private void SetItemIcon(Sprite icon)
    {
        if (Icon != null)
            Icon.SetSprite(icon);
    }
}