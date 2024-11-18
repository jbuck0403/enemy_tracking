using System;
using UnityEngine;

public class Shield : Character
{
    [NonSerialized]
    public Transform shieldFrom;

    [NonSerialized]
    public Transform shielding;

    public void SetMaster(Transform master)
    {
        shielding = master;

        if (shielding.CompareTag(CommonTags.Enemy))
        {
            shieldFrom = GameObject.FindGameObjectWithTag(CommonTags.Player).transform;
        }
        else if (shielding.CompareTag(CommonTags.Player))
        {
            shieldFrom = master;
        }
    }

    protected override void FixedUpdate()
    {
        if (shielding != null)
        {
            FollowTarget(shielding);
        }
        if (shieldFrom != null)
        {
            FaceTarget(shieldFrom);
        }
        base.FixedUpdate();
    }
}
