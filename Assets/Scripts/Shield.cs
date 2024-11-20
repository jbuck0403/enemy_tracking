using System;
using UnityEngine;

public class Shield : Character
{
    [SerializeField]
    public static readonly GameObject shieldPrefab;

    [SerializeField]
    private Transform shieldFrom;

    [SerializeField]
    private Transform shielding;

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

    private void ShieldMovement(Vector2 offsetDirection)
    {
        if (shielding != null)
        {
            FollowTarget(shielding, 15f, offsetDirection);
        }
        if (shieldFrom != null)
        {
            FaceTarget(shieldFrom);
        }
    }

    protected override void FixedUpdate()
    {
        ShieldMovement(Vector2.up);
        base.FixedUpdate();
    }
}
