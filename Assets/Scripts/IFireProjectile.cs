using UnityEngine;

public interface IFireProjectile
{
    // Shoot a projectile in the specified direction (or use default direction if none provided)
    Projectile ShootProjectile(Vector2 shootDirection = default);

    // Shoot projectiles at a fixed rate
    void ShootProjectileWithRateOfFire();
}
