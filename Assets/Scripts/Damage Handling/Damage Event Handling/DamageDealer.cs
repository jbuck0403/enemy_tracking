using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private DamageData GenerateDamageData(Projectile projectile)
    {
        int rawDamageDealt = projectile.Damage;
        GameObject damageDealer = projectile.FiredByGameObject;
        GameObject damageReceiver = gameObject;
        DamageType damageType = projectile.Type;
        return new DamageData(rawDamageDealt, damageDealer, damageReceiver, damageType);
    }
}
