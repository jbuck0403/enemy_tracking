using UnityEngine;

public class Enemy : Combatant
{
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
}
