using UnityEngine;

public class FireBomb : FireProjectile, IFireProjectile
{
    [SerializeField]
    private int numProjectiles = 5;
    private Vector2[] directions;

    protected override void Awake()
    {
        base.Awake();
        directions = GetProjectileDirections();
    }

    public void FireProjectile()
    {
        Explode();
    }

    public void FireProjectileWithRateOfFire()
    {
        Explode();
    }

    private void Explode()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            ShootProjectile(directions[i]);
        }

        Destroy(gameObject);
    }

    private Vector2[] GetProjectileDirections()
    {
        Vector2[] directions = new Vector2[numProjectiles];

        // Calculate angle between each projectile (in radians)
        float angleStep = 2f * Mathf.PI / numProjectiles;

        for (int i = 0; i < numProjectiles; i++)
        {
            // Calculate angle for this projectile
            float angle = i * angleStep;

            // Convert angle to direction vector
            // Using sin/cos gives us points on a circle
            directions[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        return directions;
    }
}
