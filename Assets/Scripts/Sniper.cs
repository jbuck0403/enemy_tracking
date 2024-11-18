using System;
using UnityEngine;

public class Sniper : Enemy
{
    private void FollowTargetAction()
    {
        moving = true;
        MoveInDirection(-transform.up);
    }

    protected override void FollowTarget(
        Transform target,
        Action targetWithinDistanceFn = null,
        float offset = 0f,
        Vector2? offsetDirection = null
    )
    {
        base.FollowTarget(target, FollowTargetAction);
    }
}
