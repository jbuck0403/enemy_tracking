using UnityEngine;

[RequireComponent(typeof(Health))]
public class DropCollectible : MonoBehaviour
{
    [SerializeField]
    private GameObject[] collectibles;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void SpawnRandomCollectible()
    {
        int rand = Random.Range(0, collectibles.Length);

        Instantiate(collectibles[rand], transform.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        health.OnDeath += SpawnRandomCollectible;
    }

    private void OnDisable()
    {
        health.OnDeath -= SpawnRandomCollectible;
    }
}
