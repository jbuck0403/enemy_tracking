using UnityEngine;

public class DamageData
{
    public float RawDamageDealt { get; }
    public GameObject DamageDealer { get; }
    public GameObject DamageReceiver { get; }
    public DamageType DamageType { get; }

    public DamageData(
        float rawDamageDealt,
        GameObject damageDealer,
        GameObject damageReceiver,
        DamageType damageType
    )
    {
        RawDamageDealt = rawDamageDealt;
        DamageDealer = damageDealer;
        DamageReceiver = damageReceiver;
        DamageType = damageType;
    }
}

public class DamageType
{
    public const string Physical = "Physical";
}
