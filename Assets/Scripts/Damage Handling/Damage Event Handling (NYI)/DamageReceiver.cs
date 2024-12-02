using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    private DamageEventChannel damageChannel;

    [SerializeField]
    private DamageHandlerStrategy damageHandlerStrategy;

    private DamageHandlerBase damageHandler;
    private Health health;

    private void Awake()
    {
        // Cache components
        damageHandler = GetComponent<DamageHandlerBase>();
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        damageChannel.OnDamageDealt += HandleDamage;
    }

    private void OnDisable()
    {
        damageChannel.OnDamageDealt -= HandleDamage;
    }

    private void HandleDamage(DamageData data)
    {
        if (data.DamageReceiver == gameObject)
        {
            damageHandlerStrategy.HandleDamage(data, damageHandler, health);
        }
    }
}
