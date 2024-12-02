using UnityEngine;

[CreateAssetMenu(
    fileName = "NewDamageHandlerStrategy",
    menuName = "DamageHandler/Strategy",
    order = 2
)]
public class DamageHandlerStrategy : ScriptableObject, IDamageHandler
{
    [SerializeField]
    private float damageMultiplier = 1f;

    public void HandleDamage(
        DamageData data,
        DamageHandlerBase damageHandlerBase,
        TakeDamage takeDamage
    )
    {
        // calculate the final damage based on the characters strategy pattern
        float finalDamage = damageHandlerBase.CalculateDamage(
            data.RawDamageDealt,
            damageMultiplier
        );

        // apply the damage
        takeDamage.ApplyDamage(new Projectile(), (int)finalDamage);
    }
}
