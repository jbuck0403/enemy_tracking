using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    protected Transform playerTransform;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag(CommonTags.Player).transform;

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
