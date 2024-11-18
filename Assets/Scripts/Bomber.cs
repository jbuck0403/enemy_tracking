using UnityEngine;

public class Bomber : Enemy
{
    [SerializeField]
    private int numProjectiles = 5;
    private Vector2[] directions;

    protected override void Start()
    {
        directions = GetProjectileDirections();
        base.Start();
    }

    protected override void ShootPlayer()
    {
        if (TargetWithinDetectionRange(playerTransform))
        {
            Explode();
        }
    }

    private void Explode()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            ShootProjectile(directions[i]);
        }

        Die();
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
