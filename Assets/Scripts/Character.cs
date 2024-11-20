using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;

    [NonSerialized]
    public bool moving = false;

    protected int CurrentHealth { get; private set; }

    [SerializeField]
    protected int maxHealth = 10;

    [SerializeField]
    protected float decelerationRate = 1f;

    [SerializeField]
    protected float decelerationFloor = 1f;

    [SerializeField]
    protected float speed = 5f;

    [SerializeField]
    protected float maxSpeed = 5f;

    [SerializeField]
    protected float acceleration = 5f;

    [SerializeField]
    protected float maxRotationSpeed = 360f;

    [SerializeField]
    protected float detectionRange = 30f;

    [SerializeField]
    protected float detectionRangeTolerance = 3f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
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
        float maxRotationThisFrame = maxRotationSpeed * Time.fixedDeltaTime;

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

    protected virtual void FollowTarget(
        Transform target,
        Action targetWithinDistanceFn = null,
        float offset = 0f,
        Vector2? offsetDirection = null
    )
    {
        if (target == null)
            return;

        // Calculate offset position if direction is provided
        Vector2 targetPosition = target.position;
        if (offsetDirection.HasValue && offset != 0f)
        {
            targetPosition += offsetDirection.Value.normalized * offset;
        }

        FaceTarget(target);

        bool withinTolerableRange = TargetWithinTolerableRange(target, null, targetPosition);
        float distanceToTarget = DistanceToPosition(targetPosition);

        // Target is ideally close
        if (withinTolerableRange)
        {
            moving = false;
            return;
        }
        else
        {
            // Target is still within detection range
            if (TargetWithinDetectionRange(target, null, targetPosition))
            {
                if (targetWithinDistanceFn != null)
                {
                    // Unless action is provided, do nothing
                    targetWithinDistanceFn?.Invoke();
                }
                else
                {
                    moving = false;
                }
            }
            // Target is too far away
            else
            {
                moving = true;
                MoveInDirection(transform.up);
            }
        }
    }

    private float DistanceToPosition(Vector2 positionToCheck)
    {
        return ((Vector2)transform.position - positionToCheck).sqrMagnitude;
    }

    protected bool TargetWithinTolerableRange(
        Transform target,
        float? rangeOverride = null,
        Vector2? targetPosition = null
    )
    {
        if (target == null)
            return false;

        Vector2 positionToCheck = targetPosition ?? (Vector2)target.position;
        float distance = DistanceToPosition(positionToCheck);
        float range = rangeOverride ?? detectionRange;

        return distance >= range - detectionRangeTolerance
            && distance <= range + detectionRangeTolerance;
    }

    protected bool TargetWithinDetectionRange(
        Transform target,
        float? rangeOverride = null,
        Vector2? targetPosition = null
    )
    {
        if (target == null)
            return false;

        Vector2 positionToCheck = targetPosition ?? (Vector2)target.position;
        float distance = DistanceToPosition(positionToCheck);
        float range = rangeOverride ?? detectionRange;

        print($"{distance} || {range}");

        return distance <= range + detectionRangeTolerance;
    }

    private void SlowToStop()
    {
        rb.velocity = Vector2.Lerp(
            rb.velocity,
            Vector2.zero,
            decelerationRate * Time.fixedDeltaTime
        );

        if (rb.velocity.sqrMagnitude < decelerationFloor)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(Projectile projectile, int? damageOverride = null)
    {
        if (projectile == null)
            return;

        string firedBy = projectile.FiredBy;

        if (!gameObject.CompareTag(firedBy))
        {
            int damage = damageOverride ?? projectile.Damage;
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth <= 0)
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

            if (!gameObject.CompareTag(projectile.FiredBy))
            {
                TakeDamage(projectile);
                projectile.DestroyProjectile();
            }
        }
    }
}
