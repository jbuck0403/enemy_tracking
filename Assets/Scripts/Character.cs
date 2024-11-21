using System;
using UnityEngine;

public class Character : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private CharacterType characterType;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private WithinDistance withinDistanceFn;

    [NonSerialized]
    public bool moving = false;

    protected int CurrentHealth { get; private set; }

    public event Action OnDamageTaken;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        withinDistanceFn = GetComponent<WithinDistance>();
    }

    protected virtual void Start()
    {
        CurrentHealth = characterType.MaxHealth;
        OnDamageTaken += UpdateColorByHealth;
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
        float maxRotationThisFrame = characterType.MaxRotationSpeed * Time.fixedDeltaTime;

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
        Vector2 targetVelocity = normalizedDirection * characterType.Speed;

        // Calculate the difference between current and target velocity
        Vector2 velocityDiff = targetVelocity - rb.velocity;

        // Apply acceleration towards the target velocity
        Vector2 accelerationForce = velocityDiff * characterType.Acceleration;
        rb.AddForce(accelerationForce);

        // Cap the velocity at maxSpeed
        if (rb.velocity.magnitude > characterType.MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * characterType.MaxSpeed;
        }
    }

    protected virtual void FollowTarget(
        Transform target,
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
                if (withinDistanceFn != null)
                {
                    // Unless action is provided, do nothing
                    withinDistanceFn?.WithinDistanceAction();
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
        float range = rangeOverride ?? characterType.DetectionRange;

        return distance >= range - characterType.DetectionRangeTolerance
            && distance <= range + characterType.DetectionRangeTolerance;
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
        float range = rangeOverride ?? characterType.DetectionRange;

        // print($"{distance} || {range}");

        return distance <= range + characterType.DetectionRangeTolerance;
    }

    private void SlowToStop()
    {
        rb.velocity = Vector2.Lerp(
            rb.velocity,
            Vector2.zero,
            characterType.DecelerationRate * Time.fixedDeltaTime
        );

        if (rb.velocity.sqrMagnitude < characterType.DecelerationFloor)
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

            // Invoke any subscribers to OnDamageTaken
            OnDamageTaken?.Invoke();

            if (CurrentHealth <= 0)
            {
                Die();
                //game over screen from gamemanager
            }
        }
    }

    private void UpdateColorByHealth()
    {
        if (spriteRenderer != null)
        {
            // Lerp from red (low health) to green (full health)
            float healthPercentage = (float)CurrentHealth / characterType.MaxHealth;
            Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

            spriteRenderer.color = healthColor;
        }
    }

    public void Die()
    {
        OnDestroy();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDamageTaken -= UpdateColorByHealth;
    }
}
