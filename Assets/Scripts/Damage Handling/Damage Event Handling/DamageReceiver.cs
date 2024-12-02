using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    private DamageEventChannel damageChannel;

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
            // Process damage
        }
    }
}
