using System;
using UnityEngine;

public class Combatant : Character, IFireProjectile
{
    public IFireProjectile[] allWeapons;

    [NonSerialized]
    public FireProjectile defaultWeapon;

    [NonSerialized]
    public IFireProjectile currentWeapon;

    public void FireProjectile() => currentWeapon.FireProjectile();

    public void FireProjectileWithRateOfFire() => currentWeapon.FireProjectileWithRateOfFire();

    public void SwapWeapon(IFireProjectile newWeapon)
    {
        currentWeapon = newWeapon;
    }

    protected override void Awake()
    {
        base.Awake();
        defaultWeapon = GetComponent<FireProjectile>();
        currentWeapon = defaultWeapon as IFireProjectile;
    }
}
