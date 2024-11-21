using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField]
    WeaponType weaponType;

    private Transform projectileSpawnPoint;

    private float timeSinceLastShot;

    private Transform projectileContainer;

    protected virtual void Awake()
    {
        projectileContainer = GameObject
            .FindGameObjectWithTag(CommonTags.ProjectileContainer)
            .transform;

        projectileSpawnPoint = transform.GetChild(0).transform;
    }

    protected virtual void ShootProjectileWithRateOfFire()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= weaponType.RateOfFire)
        {
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }

    // If no value specified for shootDirection, defaults to Vector2.zero (0,0)
    public Projectile ShootProjectile(Vector2 shootDirection = default)
    {
        if (weaponType.ProjectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is null!");
            return null;
        }

        if (projectileSpawnPoint == null)
        {
            Debug.LogError("Projectile spawn point is null!");
            return null;
        }

        if (shootDirection == default)
        {
            // Get the forward direction based on current rotation (remember we're rotated -90 on Z)
            shootDirection = transform.up;
        }

        // Calculate spawn position
        Vector2 spawnPosition = projectileSpawnPoint.position;

        // Instantiate and setup the projectile
        GameObject projectile = Instantiate(
            weaponType.ProjectilePrefab,
            spawnPosition,
            transform.rotation
        );

        projectile.transform.SetParent(projectileContainer);

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        if (projectileComponent == null)
        {
            Debug.LogError("Projectile script not found on prefab!");
            return null;
        }

        projectileComponent.SetFiredBy(gameObject.tag);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb == null)
        {
            Debug.LogError("Rigidbody2D not found on projectile!");
            return null;
        }

        // Apply velocity in the shooting direction
        projectileRb.velocity = shootDirection * weaponType.ProjectileSpeed;

        return projectileComponent;
    }
}