using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyTypes;

    private float timeSinceLastSpawn;

    [SerializeField]
    private float spawnInterval = 3f;

    void Start()
    {
        timeSinceLastSpawn = 0f;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnRandomEnemy(1);
            timeSinceLastSpawn = 0f;
        }
    }

    public void SpawnEnemy(GameObject enemy, Vector2? position = null)
    {
        Vector2 spawnPosition = position ?? Vector2.zero;
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    public void SpawnRandomEnemy(int numToSpawn, Vector2? spawnPosition = null)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnEnemy(ReturnRandomEnemy(), spawnPosition);
        }
    }

    private GameObject ReturnRandomEnemy()
    {
        int numEnemyTypes = enemyTypes.Length;
        int randomEnemyIndex = Random.Range(0, numEnemyTypes);

        return enemyTypes[randomEnemyIndex];
    }
}
