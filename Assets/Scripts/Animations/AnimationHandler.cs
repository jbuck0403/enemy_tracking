using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    protected DeathAnimation deathAnimation;
    public AnimationEventChannel animationEventChannel;

    private void OnSubscribe() { }
}
