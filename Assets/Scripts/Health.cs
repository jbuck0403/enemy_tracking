using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth = 10f;

    [NonSerialized]
    public float CurrentHealth;

    private SpriteRenderer spriteRenderer;

    public event Action OnDamageTaken;
    public event Action OnDeath;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void ApplyDamage(float damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        OnDamageTaken?.Invoke();

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Die()
    {
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

    protected virtual void SubscribeToEvents()
    {
        OnDamageTaken += UpdateColorByHealth;
        OnDeath += Die;
    }

    protected virtual void UnsubscribeFromEvents()
    {
        OnDamageTaken -= UpdateColorByHealth;
        OnDeath -= Die;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
}
