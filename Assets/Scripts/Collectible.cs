using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collectible : MonoBehaviour
{
    CollectibleBehaviorBase collectibleBehavior;

    void HandleCollectible() => collectibleBehavior.Collect();

    void Awake()
    {
        collectibleBehavior = GetComponent<CollectibleBehaviorBase>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(CommonTags.Player))
        {
            HandleCollectible();
            Destroy(gameObject);
        }
    }
}
