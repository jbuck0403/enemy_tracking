using System;
using UnityEngine;

public class Character : MonoBehaviour, IMove, ITakeDamage, IWithinDistance
{
    private WithinDistance withinDistanceFn;
    private TakeDamage damage;

    [NonSerialized]
    public Move move;

    public void ApplyDamage(Projectile projectile, int? damageOverride = null) =>
        damage.ApplyDamage(projectile, damageOverride);

    public void Die(Action OnDestroy) => damage.Die(OnDestroy);

    public void ExecuteWithinDistanceAction() => withinDistanceFn?.ExecuteWithinDistanceAction();

    public void FaceTarget(Transform target) => move.FaceTarget(target);

    public void MoveInDirection(Vector2 direction) => move.MoveInDirection(direction);

    public void FollowTarget(
        Transform target,
        float offset = 0f,
        Vector2? offsetDirection = null
    ) => move.FollowTarget(target, offset, offsetDirection);

    public bool TargetWithinTolerableRange(
        Transform target,
        float? rangeOverride = null,
        Vector2? targetPosition = null
    ) => move.TargetWithinTolerableRange(target, rangeOverride, targetPosition);

    public bool TargetWithinDetectionRange(
        Transform target,
        float? rangeOverride = null,
        Vector2? targetPosition = null
    ) => move.TargetWithinDetectionRange(target, rangeOverride, targetPosition);

    public void SlowToStop() => move.SlowToStop();

    public float DistanceToPosition(Vector2 positionToCheck) =>
        move.DistanceToPosition(positionToCheck);

    protected virtual void Awake()
    {
        damage = GetComponent<TakeDamage>();
        move = GetComponent<Move>();
        withinDistanceFn = GetComponent<WithinDistance>();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void FixedUpdate()
    {
        if (!move.moving)
        {
            SlowToStop();
        }
    }
}
