using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewDamageEventChannel",
    menuName = "DamageHandler/DamageEvent",
    order = 2
)]
public class DamageEventChannel : ScriptableObject
{
    // The event itself
    public event Action<DamageData> OnDamageDealt;

    // Method to raise the event
    public void RaiseDamageEvent(DamageData data) // call from DamageHandler
    {
        OnDamageDealt?.Invoke(data);
    }
}
