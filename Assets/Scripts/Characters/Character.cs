using System;
using UnityEngine;

public class Character : MonoBehaviour, IMove
{
    private WithinDistance withinDistanceFn;
    private Collider2D col;
    private Rigidbody2D rb;

    [SerializeField]
    private bool _active = true;
    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            if (col != null)
                col.enabled = value;
            if (rb != null)
                rb.simulated = value;
        }
    }

    [NonSerialized]
    public Move move;

    public void FaceTarget(Transform target) => move.FaceTarget(target);

    public void MoveInDirection(Vector2 direction) => move.MoveInDirection(direction);

    public void ExecuteWithinDistanceAction()
    {
        if (withinDistanceFn == null)
        {
            return;
        }

        withinDistanceFn.ExecuteWithinDistanceAction();
    }

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
        move = GetComponent<Move>();
        withinDistanceFn = GetComponent<WithinDistance>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        if (!Active)
            return;
    }

    protected virtual void FixedUpdate()
    {
        if (!Active)
            return;

        if (!move.moving)
        {
            SlowToStop();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
