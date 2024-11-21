using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : TakeDamage, ITakeDamage
{
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
