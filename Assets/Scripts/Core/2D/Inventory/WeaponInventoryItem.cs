using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponInventoryItem : InventoryItem
{
    public int ClipCapacity;
    public int TotalAmmo;
    public float ReloadingTime = 1f;
    public float ShotsPerSecond = 5;
    public int AmmoInClip;

    public UnityEvent OnShot;
    public UnityEvent OnReload;

    private bool _isShooting;
    private bool _isReloading;

    private float reloadingStart;

    public void PullTrigger()
    {
        _isShooting = true;
    }

    public void ReleaseTrigger()
    {
        _isShooting = false;
    }

    private void Update()
    {
        if (_isShooting && !_isReloading && ActionEx.CheckCooldown(Shoot, 1 / ShotsPerSecond))
        {
            Shoot();
        }

        UpdateIsReloadingState();
    }

    private void Shoot()
    {
        if (AmmoInClip > 0)
        {
            TotalAmmo--;
            AmmoInClip--;
            OnShot?.Invoke();
        }
    }

    public void Reload()
    {
        _isReloading = true;
        reloadingStart = Time.time;

        AmmoInClip = Mathf.Min(TotalAmmo, ClipCapacity);
        OnReload?.Invoke();
    }

    private void UpdateIsReloadingState()
    {
        if (_isReloading && Time.time - ReloadingTime > reloadingStart)
            _isReloading = false;
    }
}