using UnityEngine;

public class Enemy : Combatant
{
    [SerializeField]
    private readonly float separationDistance = 1f;

    [SerializeField]
    private LayerMask shipLayer;
    private GameObject player;
    public bool IsDying { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(CommonTags.Player);
    }

    protected override void Update()
    {
        base.Update();

        if (IsDying)
        {
            return;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(CommonTags.Player);

            if (player == null)
            {
                Active = false;
                return;
            }
        }
        else if (!Active)
        {
            Active = true;
        }

        ShootPlayer();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (IsDying)
        {
            return;
        }

        if (player != null)
        {
            ApplyLateralForce();
            FollowTarget(player.transform);
        }
    }

    protected virtual void ShootPlayer()
    {
        if (TargetWithinDetectionRange(player.transform))
        {
            FireProjectileWithRateOfFire();
        }
    }

    protected bool IsShipTooClose(out Enemy nearbyEnemy)
    {
        nearbyEnemy = null;
        Collider2D[] nearbyShips = Physics2D.OverlapCircleAll(
            transform.position,
            separationDistance,
            shipLayer
        );

        foreach (var ship in nearbyShips)
        {
            if (ship.gameObject != gameObject) // Skip self
            {
                nearbyEnemy = ship.GetComponent<Enemy>();
                return true;
            }
        }

        return false;
    }

    private void ApplyLateralForce()
    {
        Enemy otherShip;
        if (IsShipTooClose(out otherShip))
        {
            Vector2 lateralForce = Vector2.Perpendicular(transform.up);

            // If the other ship is moving laterally, move in the opposite direction
            Rigidbody2D otherRb = otherShip.GetComponent<Rigidbody2D>();
            if (Mathf.Abs(otherRb.velocity.x) > 0.1f) // Check if other ship is moving laterally
            {
                if (otherRb.velocity.x > 0)
                {
                    lateralForce = -lateralForce; // Move left if other ship is moving right
                }
            }
            else // Default to position-based if other ship isn't moving laterally
            {
                if (transform.position.x < player.transform.position.x)
                {
                    lateralForce = -lateralForce;
                }
            }

            MoveInDirection(lateralForce);
        }
    }
}
