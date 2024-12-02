using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField]
    private DamageEventChannel damageEventChannel;

    private DamageData GenerateDamageData(Projectile projectile)
    {
        return new DamageData(
            projectile.Damage,
            projectile.FiredByGameObject,
            gameObject,
            projectile.Type
        );
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(CommonTags.Projectile))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            // Friendly fire check
            if (!gameObject.CompareTag(projectile.FiredBy))
            {
                DamageData damageData = GenerateDamageData(projectile);

                damageEventChannel.RaiseDamageEvent(damageData);
                projectile.DestroyProjectile();
            }
        }
    }
}
