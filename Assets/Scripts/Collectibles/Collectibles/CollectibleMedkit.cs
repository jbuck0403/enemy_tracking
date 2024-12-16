using UnityEngine;

public class CollectibleMedkit : Collectible
{
    [SerializeField]
    private float healAmount = 5f;
    Health health;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(CommonTags.Player);
        if (player != null)
        {
            health = player.GetComponent<Health>();
        }
    }

    protected override void HandleCollectible()
    {
        if (health == null)
        {
            return;
        }

        health.Heal(healAmount);
    }
}
