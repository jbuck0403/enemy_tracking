using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Collectible : MonoBehaviour
{
    protected abstract void HandleCollectible();

    public void Collect()
    {
        HandleCollectible();
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
