using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageEventChannel : MonoBehaviour
{
    // The event itself
    public event Action<DamageData> OnDamageDealt;

    // Method to raise the event
    public void RaiseDamageEvent(DamageData data)
    {
        OnDamageDealt?.Invoke(data);
    }
}
