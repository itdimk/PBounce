using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryItemDisplay : InventoryItemDisplay
{
    public NumberDisplay TotalAmmo;
    public NumberDisplay AmmoInClip;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void SetItem(WeaponInventoryItem item)
    {
        SetItemID(item.ID);

        if (item != null)
        {
            SetTotalAmmo(item.TotalAmmo);
            SetAmmoInClip(item.AmmoInClip);
        }
        else
        {
            SetTotalAmmo(0);
            SetAmmoInClip(0);
        }
    }

    private void SetTotalAmmo(int count)
    {
        if (TotalAmmo != null)
            TotalAmmo.SetNumber(count);
    }

    private void SetAmmoInClip(int count)
    {
        if (AmmoInClip != null)
            AmmoInClip.SetNumber(count);
    }
}