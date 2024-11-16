using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string FiredBy { private set; get; } = "";
    public int Damage { protected set; get; } = 1;
    public float DestructionDelay { get; set; } = 0f;

    public void SetFiredBy(string firedByTag)
    {
        FiredBy = firedByTag;
    }

    public void DestroyProjectile()
    {
        StartCoroutine(DestroyProjectileCoRoutine());
    }

    private IEnumerator DestroyProjectileCoRoutine()
    {
        if (DestructionDelay > 0)
        {
            yield return new WaitForSeconds(DestructionDelay);
        }

        Destroy(gameObject);
    }
}
