using UnityEngine;

public class FireBomb : FireProjectile, IFireProjectile
{
    [SerializeField]
    private int numProjectiles = 5;
    private DamageHandler damageHandler;
    private Vector2[] directions;
    private Health health;

    protected override void Awake()
    {
        base.Awake();
        directions = GetProjectileDirections();
        health = GetComponent<Health>();
        damageHandler = GetComponent<DamageHandler>();
    }

    void Start() { }

    public void UpdateNumProjectile(float num)
    {
        numProjectiles = Mathf.RoundToInt(num);
        directions = GetProjectileDirections();
    }

    public void MatchNumProjectilesToHealth(DamageData data)
    {
        UpdateNumProjectile(health.CurrentHealth);
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

    private void OnEnable()
    {
        damageHandler.damageEventChannel.OnDamageDealt += MatchNumProjectilesToHealth;
    }

    private void OnDisable()
    {
        damageHandler.damageEventChannel.OnDamageDealt -= MatchNumProjectilesToHealth;
    }
}
