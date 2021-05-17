using System;

public interface WeaponInterface
{
    /// <summary>
    /// Max ammo in a mag
    /// </summary>
    public int magazineCapacity
    {
        get;
    }

    /// <summary>
    /// Total ammo in current mag
    /// </summary>
    public int ammoInMagazine
    {
        get;
        set;
    }

    /// <summary>
    /// Total ammo player has for current weapon
    /// </summary>
    public int totalAmmo
    {
        get;
        set;
    }

    /// <summary>
    /// Used to calculate the rate of fire
    /// </summary>
    public int shotsPerSecond
    {
        get;
    }

    /// <summary>
    /// Get minimum damage
    /// Used for randomising the ammount of damage a shot can do
    /// </summary>
    public int minDamage
    {
        get;
    }

    /// <summary>
    /// Get maximum damage
    /// Used for randomising the ammount of damage a shot can do
    /// </summary>
    public int maxDamage
    {
        get;
    }

    public int getMagazineCapacity();

    public int getAmmoInMagezine();

    public void setAmmoInMagezine(int ammo);

    public int getTotalAmmo();

    public void setTotalAmmo(int total);

    public void setRemainingAmmo(int remaining);

    public float getRateOfFire();

    public bool canReload();

    public void reload();

    public int getDamage();
}
