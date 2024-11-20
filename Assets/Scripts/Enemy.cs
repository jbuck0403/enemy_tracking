using UnityEngine;

public class Enemy : Character, IFireProjectile
{
    protected Transform playerTransform;

    private FireProjectile defaultWeapon;

    private IFireProjectile currentWeapon;

    public void FireProjectile() => currentWeapon.FireProjectile();

    public void FireProjectileWithRateOfFire() => currentWeapon.FireProjectileWithRateOfFire();

    public void SwapWeapon(IFireProjectile newWeapon)
    {
        currentWeapon = newWeapon;
    }

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag(CommonTags.Player).transform;
        defaultWeapon = GetComponent<FireProjectile>();
        currentWeapon = defaultWeapon as IFireProjectile;
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
            FireProjectileWithRateOfFire();
        }
    }
}
