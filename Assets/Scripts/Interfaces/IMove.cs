using UnityEngine;

public interface IMove
{
    void ExecuteWithinDistanceAction();
    void FaceTarget(Transform target);
    void MoveInDirection(Vector2 direction);
    void FollowTarget(Transform target, float offset = 0f, Vector2? offsetDirection = null);
    bool TargetWithinTolerableRange(
        Transform target,
        float? rangeOverride = null,
        Vector2? targetPosition = null
    );
    bool TargetWithinDetectionRange(
        Transform target,
        float? rangeOverride = null,
        Vector2? targetPosition = null
    );
    void SlowToStop();
    float DistanceToPosition(Vector2 positionToCheck);
}
