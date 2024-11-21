using System;

public interface ITakeDamage
{
    public void ApplyDamage(Projectile projectile, int? damageOverride = null);
    public void Die(Action OnDestroy);
}
