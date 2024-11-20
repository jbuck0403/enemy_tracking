using UnityEngine;

public class Enemy : Character, IFireProjectile
{
    protected Transform playerTransform;

    private IFireProjectile currentWeapon;

    public Projectile ShootProjectile(Vector2 shootDirection = default) =>
        currentWeapon.ShootProjectile(shootDirection);

    public void ShootProjectileWithRateOfFire() => currentWeapon.ShootProjectileWithRateOfFire();

    public void SwapWeapon(IFireProjectile newWeapon)
    {
        currentWeapon = newWeapon;
    }

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag(CommonTags.Player).transform;
        currentWeapon = GetComponent<FireProjectile>();

        decelerationFloor = 10f;
    }

    protected override void Update()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag(CommonTags.Player).transform;
        }

        ShootPlayer();
    }

    protected override void FixedUpdate()
    {
        FollowTarget(playerTransform);
        base.FixedUpdate();
    }

    protected virtual void ShootPlayer()
    {
        if (TargetWithinDetectionRange(playerTransform))
        {
            ShootProjectileWithRateOfFire();
        }
    }
}
