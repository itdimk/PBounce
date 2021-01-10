public class WeaponItemDisplay : InventoryItemDisplay
{
    public NumberDisplay AmmoInClip;
    public NumberDisplay TotalAmmo;

    public void SetItem(WeaponInventoryItem item)
    {
        base.SetItem(item);

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
            AmmoInClip.SetNumber(count);
    }

    private void SetTotalAmmo(int count)
    {
        if (TotalAmmo != null)
            TotalAmmo.SetNumber(count);
    }
}