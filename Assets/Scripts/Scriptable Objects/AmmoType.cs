using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmoType", menuName = "Weapons/Ammo Type", order = 1)]
public class AmmoType : ScriptableObject
{
    [SerializeField]
    private DamageType damageType;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private GameObject projectilePrefab;

    public DamageType Type => damageType;
    public float ProjectileSpeed => projectileSpeed;
    public GameObject ProjectilePrefab => projectilePrefab;
}
