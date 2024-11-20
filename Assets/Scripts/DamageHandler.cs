using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private ITakeDamage damageableEntity;

    private void Awake()
    {
        damageableEntity = GetComponent<ITakeDamage>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(CommonTags.Projectile))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            if (!gameObject.CompareTag(projectile.FiredBy))
            {
                damageableEntity.TakeDamage(projectile);
                projectile.DestroyProjectile();
            }
        }
    }
}
