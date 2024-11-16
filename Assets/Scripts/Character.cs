using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private const int MAX_HEALTH = 10;
    public int currentHealth;

    [NonSerialized]
    public bool moving = false;

    private Rigidbody2D rb;

    [SerializeField]
    public float decelerationRate = 1f;

    [SerializeField]
    public float decelerationFloor = 1f;

    [SerializeField]
    protected float speed = 5f;

    [SerializeField]
    protected float maxSpeed = 5f;

    [SerializeField]
    protected float acceleration = 5f;

    [SerializeField]
    protected float maxRotationSpeed = 360f;

    [SerializeField]
    protected float projectileSpeed = 10f;

    [SerializeField]
    //shots per second
    protected float rateOfFire = 1f;

    [SerializeField]
    protected GameObject projectilePrefab;

    [SerializeField]
    private Transform projectileSpawnPoint;

    private float timeSinceLastShot;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        currentHealth = MAX_HEALTH;
    }

    protected virtual void Update() { }

    protected virtual void FixedUpdate()
    {
        if (!moving)
        {
            SlowToStop();
        }
    }

    public void FaceTarget(Transform target)
    {
        // Calculate direction vector from this object to target
        Vector2 direction = (target.position - transform.position).normalized;

        // Calculate target angle in degrees
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Get current rotation angle
        float currentAngle = transform.eulerAngles.z;

        // Calculate shortest rotation difference (-180 to +180)
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        // Calculate max rotation this frame
        float maxRotationThisFrame = maxRotationSpeed * Time.deltaTime;

        // Clamp rotation to max speed
        float rotationAmount = Mathf.Clamp(
            angleDifference,
            -maxRotationThisFrame,
            maxRotationThisFrame
        );

        // Apply rotation
        float newAngle = currentAngle + rotationAmount;
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    public void MoveInDirection(Vector2 direction)
    {
        // Normalize the input direction
        Vector2 normalizedDirection = direction.normalized;

        // Calculate target velocity based on direction and speed
        Vector2 targetVelocity = normalizedDirection * speed;

        // Calculate the difference between current and target velocity
        Vector2 velocityDiff = targetVelocity - rb.velocity;

        // Apply acceleration towards the target velocity
        Vector2 accelerationForce = velocityDiff * acceleration;
        rb.AddForce(accelerationForce);

        // Cap the velocity at maxSpeed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void SlowToStop()
    {
        rb.velocity = Vector2.Lerp(
            rb.velocity,
            Vector2.zero,
            decelerationRate * Time.fixedDeltaTime
        );
        if (tag == CommonTags.Enemy)
            print($"{tag} Velocity: {rb.velocity.sqrMagnitude}");
        if (rb.velocity.sqrMagnitude < decelerationFloor)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void ShootProjectileWithRateOfFire()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= rateOfFire)
        {
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }

    public void ShootProjectile()
    {
        // Get the forward direction based on current rotation (remember we're rotated -90 on Z)
        Vector2 shootDirection = transform.up; // In 2D, 'up' is actually forward due to our -90 rotation

        // Calculate spawn position (slightly in front of the character)
        Vector2 spawnPosition = projectileSpawnPoint.position;

        // Instantiate and setup the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);
        Projectile projectileMethods = projectile.GetComponent<Projectile>();

        projectileMethods.SetFiredBy(gameObject.tag);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        // Apply velocity in the shooting direction
        projectileRb.velocity = shootDirection * projectileSpeed;
    }

    public void TakeDamage(Projectile projectile)
    {
        if (projectile == null)
            return;

        string firedBy = projectile.FiredBy;

        if (!gameObject.CompareTag(firedBy))
        {
            int damageTaken = projectile.Damage;

            currentHealth -= damageTaken;
            print($"Health: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
                //game over screen from gamemanager
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(CommonTags.Projectile))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            TakeDamage(projectile);
            projectile.DestroyProjectile();
        }
    }
}
