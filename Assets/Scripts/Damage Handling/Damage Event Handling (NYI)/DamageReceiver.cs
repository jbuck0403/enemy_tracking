using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    private DamageEventChannel damageChannel;

    [SerializeField]
    private DamageHandlerStrategy damageHandlerStrategy;

    private DamageHandlerBase damageHandler;
    private TakeDamage takeDamage;

    private void Awake()
    {
        // Cache components
        damageHandler = GetComponent<DamageHandlerBase>();
        takeDamage = GetComponent<TakeDamage>();
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
        if (data.DamageReceiver == this.gameObject)
        {
            damageHandlerStrategy.HandleDamage(data, damageHandler, takeDamage);
        }
    }
}
