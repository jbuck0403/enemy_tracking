using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth = 10f;

    [NonSerialized]
    public float CurrentHealth;

    private SpriteRenderer spriteRenderer;
    private Color spriteBaseColor;
    private Animator deathAnimator;
    private Character character;

    public event Action OnDamageTaken;
    public event Action OnDeath;
    public event Action OnHealthChange;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        deathAnimator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        spriteBaseColor = spriteRenderer.color;
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

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        OnHealthChange?.Invoke();
    }

    public void OnDeathAnimationComplete()
    {
        character.Die();
    }

    public void HandleDeath()
    {
        character.Active = false;
        if (character is Enemy enemy)
        {
            enemy.IsDying = true;
        }

        ResetColor();
        deathAnimator.SetTrigger("DeathTrigger");
    }

    private void UpdateColorByHealth()
    {
        if (spriteRenderer != null)
        {
            // Lerp from green (full health) to red (low health)
            float healthPercentage = (float)CurrentHealth / MaxHealth;
            Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

            spriteRenderer.color = healthColor;
        }
    }

    private void ResetColor()
    {
        spriteRenderer.color = spriteBaseColor;
    }

    protected virtual void SubscribeToEvents()
    {
        OnDamageTaken += UpdateColorByHealth;
        OnHealthChange += UpdateColorByHealth;
        OnDeath += HandleDeath;
    }

    protected virtual void UnsubscribeFromEvents()
    {
        OnDamageTaken -= UpdateColorByHealth;
        OnHealthChange -= UpdateColorByHealth;
        OnDeath -= HandleDeath;
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
