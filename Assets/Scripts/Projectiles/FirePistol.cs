using UnityEngine;

public class FirePistol : FireProjectile, IFireProjectile
{
    // Shoot projectiles at a fixed rate
    public void FireProjectile()
    {
        ShootProjectile();
    }

    public void FireProjectileWithRateOfFire()
    {
        ShootProjectileWithRateOfFire();
    }
}
