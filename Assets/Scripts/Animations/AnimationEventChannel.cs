using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewAnimationEventChannel",
    menuName = "EventChannels/AnimationEvent",
    order = 2
)]
public class AnimationEventChannel : ScriptableObject
{
    // The event itself
    public event Action OnDamageAnimation;
    public event Action OnDeathAnimation;

    // Method to raise the event
    public void RaiseDamageAnimationEvent()
    {
        OnDamageAnimation?.Invoke();
    }

    public void RaiseDeathAnimationEvent()
    {
        OnDeathAnimation?.Invoke();
    }
}
