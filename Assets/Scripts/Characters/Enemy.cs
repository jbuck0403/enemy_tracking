using UnityEngine;

public class Enemy : Combatant
{
    private GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(CommonTags.Player);
    }

    protected override void Update()
    {
        base.Update();

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
