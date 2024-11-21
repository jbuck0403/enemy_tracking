using System;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    public int MaxHealth = 10;
    public int CurrentHealth;

    private SpriteRenderer spriteRenderer;

    public event Action OnDamageTaken;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        OnDamageTaken += UpdateColorByHealth;
        CurrentHealth = MaxHealth;
    }

    public void ApplyDamage(Projectile projectile, int? damageOverride = null)
    {
        if (projectile == null)
            return;

        string firedBy = projectile.FiredBy;

        if (!gameObject.CompareTag(firedBy))
        {
            int damage = damageOverride ?? projectile.Damage;
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            // Invoke any subscribers to OnDamageTaken
            OnDamageTaken?.Invoke();

            if (CurrentHealth <= 0)
            {
                Die();
                //game over screen from gamemanager
            }
        }
    }

    public void Die(Action OnDestroy = null)
    {
        OnDestroy?.Invoke();

        Destroy(gameObject);
    }

    private void UpdateColorByHealth()
    {
        if (spriteRenderer != null)
        {
            // Lerp from red (low health) to green (full health)
            float healthPercentage = (float)CurrentHealth / MaxHealth;
            Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

            spriteRenderer.color = healthColor;
        }
    }

    protected virtual void OnDestroy()
    {
        OnDamageTaken -= UpdateColorByHealth;
    }
}
