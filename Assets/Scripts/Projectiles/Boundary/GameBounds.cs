using UnityEngine;

public class GameBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(CommonTags.Projectile))
        {
            other.GetComponent<Projectile>().DestroyProjectile();
        }
    }
}
