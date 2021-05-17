using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponKalashnikov : WeaponInterface
{
    private int _magazineCapacity = 30;
    public int magazineCapacity {
        get => _magazineCapacity;
    }

    private int _ammoInMagazine = 30;
    public int ammoInMagazine {
        get => _ammoInMagazine;
        set => _ammoInMagazine = value;
    }

    private int _totalAmmo = 120;
    public int totalAmmo {
        get => _totalAmmo;
        set => _totalAmmo = value;
    }

    private int _shotsPerSecond = 15;
    public int shotsPerSecond {
        get => _shotsPerSecond;
    }

    private int _minDamage = 20;
    public int minDamage
    {
        get => _minDamage;
    }

    private int _maxDamage = 45;
    public int maxDamage
    {
        get => _maxDamage;
    }

    public int getMagazineCapacity()
    {
        return magazineCapacity;
    }

    public float getRateOfFire()
    {
        return 1.0f / shotsPerSecond;
    }

    public int getAmmoInMagezine()
    {
        return ammoInMagazine;
    }

    public void setAmmoInMagezine(int ammo)
    {
        ammoInMagazine = ammo;
    }

    public int getTotalAmmo()
    {
        return totalAmmo;
    }

    public void setTotalAmmo(int total)
    {
        totalAmmo = total;
    }

    public void setRemainingAmmo(int remaining)
    {
        ammoInMagazine = remaining;
    }

    public bool canReload()
    {
        return totalAmmo > 0 && getAmmoInMagezine() < getMagazineCapacity();
    }

    public void reload()
    {
        int deducted = getTotalAmmo() - getMagazineCapacity();
        int magReload = getMagazineCapacity();
        if (deducted >= 0)
        {
            setTotalAmmo(deducted);
            setAmmoInMagezine(magReload);
        } else
        {
            setTotalAmmo(0);
            magReload += deducted;
            setAmmoInMagezine(magReload);
        }
    }

    public int getDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}
