using UnityEngine;

[RequireComponent(typeof(Collectible))]
public abstract class CollectibleBehaviorBase : MonoBehaviour
{
    protected abstract void HandleCollectible();

    public void Collect()
    {
        HandleCollectible();
    }
}
