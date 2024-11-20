public interface ITakeDamage
{
    public void TakeDamage(Projectile projectile, int? damageOverride = null);
    public void Die();
}
