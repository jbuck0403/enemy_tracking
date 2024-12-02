using UnityEngine;

[CreateAssetMenu(
    fileName = "NewDamageHandlerStrategy",
    menuName = "DamageHandler/Strategy",
    order = 1
)]
public class DamageHandlerStrategy : ScriptableObject
{
    [SerializeField]
    private float damageMultiplier = 1f;

    public void HandleDamage(DamageData data, DamageHandlerBase damageHandlerBase, Health health)
    {
        // calculate the final damage based on the characters strategy pattern
        float finalDamage = damageHandlerBase.CalculateDamage(
            data.RawDamageDealt,
            damageMultiplier
        );

        // apply the damage
        health.ApplyDamage(finalDamage);
    }
}
