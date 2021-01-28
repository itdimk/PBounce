using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string ID;
    public string DisplayName;
    public string Description;
    public string Category;

    public Sprite Icon;
    public Color IconColor = Color.white;
    public bool IsHidden = true;
    public int Count = 1;
    public bool QuickAccess;
}