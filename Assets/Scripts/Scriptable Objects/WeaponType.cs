using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponType", menuName = "Weapons/Weapon Type", order = 1)]
public class WeaponType : ScriptableObject
{
    [SerializeField]
    [Tooltip("shots per second\n(0.5 == 2 shots per second, 2 == 1 shot every 2 seconds)")]
    private float rateOfFire = 1f;

    public float RateOfFire => rateOfFire;
}
