using System.Collections;
using UnityEngine;

public class CollectibleGattlingGun : Collectible
{
    [SerializeField]
    private WeaponType gattlingGun;

    [SerializeField]
    private float duration = 2f;

    private Combatant combatant;
    private FireProjectile fireProjectile;

    private void Awake()
    {
        combatant = GameObject.FindGameObjectWithTag(CommonTags.Player).GetComponent<Combatant>();
    }

    protected override void HandleCollectible()
    {
        fireProjectile = combatant.currentWeapon as FireProjectile;

        fireProjectile.SwapThenSwapBack(gattlingGun, duration);
    }
}
