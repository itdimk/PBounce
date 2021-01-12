using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItemDisplay : Display<InventoryItem>
{
    public SpritePropertyDisplay Icon;
    public StringDisplay ItemName;
    public StringDisplay ItemDescription;
    public NumberDisplay ItemCount;
    public Button Button;

    [HideInInspector] public UnityEvent<InventoryItem> Clicked;
    [HideInInspector] public UnityEvent<InventoryItem> LongPress;

    private InventoryItem _item;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (Button == null) return;
        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(() => Clicked?.Invoke(_item));
        Button.gameObject.AddComponent<LongPressTrigger>()
            .OnLongPress.AddListener(() => LongPress?.Invoke(_item));
    }

    public override void SetItemToDisplay(InventoryItem item)
    {
        base.SetItemToDisplay(item);

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
            ItemName.SetItemToDisplay(!string.IsNullOrEmpty(value) ? value : "No item");
    }

    private void SetItemDescription(string value)
    {
        if (ItemDescription != null)
            ItemDescription.SetItemToDisplay(!string.IsNullOrEmpty(name) ? value : "No description");
    }

    private void SetItemCount(int count)
    {
        if (ItemCount != null)
            ItemCount.SetItemToDisplay(count);
    }

    private void SetItemIcon(Sprite icon)
    {
        if (Icon != null)
            Icon.SetItemToDisplay(icon);
    }
}