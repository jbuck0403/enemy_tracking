using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    Transform playerTransform;

    [SerializeField]
    protected float detectionRange = 30f;

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
        FollowPlayer();
        base.FixedUpdate();
    }

    private void FollowPlayer()
    {
        FaceTarget(playerTransform);

        if (!TargetWithinDistance(playerTransform))
        {
            moving = true;
            MoveInDirection(transform.up);
        }
        else
        {
            moving = false;
        }
    }

    private void ShootPlayer()
    {
        if (TargetWithinDistance(playerTransform))
        {
            ShootProjectileWithRateOfFire();
        }
    }

    private bool TargetWithinDistance(Transform target)
    {
        if (target == null)
            return false;

        return ((Vector2)(target.position - transform.position)).sqrMagnitude <= detectionRange;
    }
}
