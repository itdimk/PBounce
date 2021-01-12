public class WeaponItemDisplay : InventoryItemDisplay
{
    public NumberDisplay AmmoInClip;
    public NumberDisplay TotalAmmo;

    public void SetItemToDisplay(WeaponInventoryItem item)
    {
        base.SetItemToDisplay(item);

        if (item != null)
        {
            SetAmmoInClip(item.AmmoInClip);
            SetTotalAmmo(item.TotalAmmo);
        }
        else
        {
            SetAmmoInClip(0);
            SetTotalAmmo(0);
        }
    }

    private void SetAmmoInClip(int count)
    {
        if (AmmoInClip != null)
            AmmoInClip.SetItemToDisplay(count);
    }

    private void SetTotalAmmo(int count)
    {
        if (TotalAmmo != null)
            TotalAmmo.SetItemToDisplay(count);
    }
}