using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DamageType Type = DamageType.Physical;
    public string FiredBy { private set; get; } = "";
    public GameObject FiredByGameObject { private set; get; }
    public int Damage { protected set; get; } = 1;
    public float DestructionDelay { get; set; } = 0f;

    public void SetFiredBy(GameObject firedBy)
    {
        FiredBy = firedBy.tag;
        FiredByGameObject = firedBy;
    }

    public void SetDamageType(DamageType damageType)
    {
        Type = damageType;
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
