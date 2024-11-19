using System;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : Character
{
    [SerializeField]
    private static readonly GameObject shieldPrefab;

    [NonSerialized]
    public Transform shieldFrom;

    [NonSerialized]
    public Transform shielding;

    [HideInInspector]
    [SerializeField]
    protected new GameObject projectilePrefab = null;

    public static Shield CreateShield(
        Transform master,
        Vector3 spawnPosition,
        GameObject prefab = null
    )
    {
        // Use provided prefab or try to load from Resources
        prefab = prefab ?? shieldPrefab;

        if (prefab == null)
        {
            Debug.LogError("Shield prefab not found!");
            return null;
        }

        // Instantiate the shield
        GameObject instance = Instantiate(prefab, spawnPosition, Quaternion.identity);
        Shield shield = instance.GetComponent<Shield>();

        if (shield == null)
        {
            Debug.LogError("Shield component not found on prefab!");
            Destroy(instance);
            return null;
        }

        // Setup the shield
        shield.SetMaster(master);

        return shield;
    }

    public void SetMaster(Transform master)
    {
        shielding = master;

        if (shielding.CompareTag(CommonTags.Enemy))
        {
            shieldFrom = GameObject.FindGameObjectWithTag(CommonTags.Player).transform;
        }
        else if (shielding.CompareTag(CommonTags.Player))
        {
            shieldFrom = master;
        }
    }

    private void ShieldMovement()
    {
        if (shielding != null)
        {
            FollowTarget(shielding);
        }
        if (shieldFrom != null)
        {
            FaceTarget(shieldFrom);
        }
    }

    protected override void FixedUpdate()
    {
        ShieldMovement();
        base.FixedUpdate();
    }
}
