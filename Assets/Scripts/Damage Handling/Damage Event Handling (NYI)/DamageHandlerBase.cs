using UnityEngine;

public abstract class DamageHandlerBase : MonoBehaviour
{
    public float CalculateDamage(float initialDamage, float damageMultiplier)
    {
        float damage = initialDamage * damageMultiplier;

        return damage;
    }
}
