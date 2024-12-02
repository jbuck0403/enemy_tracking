using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DamageHandler))]
[RequireComponent(typeof(DamageReceiver))]
[RequireComponent(typeof(DamageHandlerDefault))]
public class DamageSystemGroup : MonoBehaviour
{
    public Health Health { get; private set; }
    public DamageHandler DamageHandler { get; private set; }
    public DamageReceiver DamageReceiver { get; private set; }
    public DamageHandlerBase damageHandlerBase { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        DamageHandler = GetComponent<DamageHandler>();
        DamageReceiver = GetComponent<DamageReceiver>();
    }
}
