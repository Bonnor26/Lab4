using UnityEngine;


public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject bigMeteorPrefab;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int meteorsBeforeBigOne = 5;
    [SerializeField] private float spawnRangeX = 8f;
    [SerializeField] private float spawnY = 7.5f;

    private int destroyedMeteorCount = 0;

    private void OnEnable()
    {
        GameEvents.RegularMeteorDestroyed += HandleRegularMeteorDestroyed;
        GameEvents.PlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.RegularMeteorDestroyed -= HandleRegularMeteorDestroyed;
        GameEvents.PlayerDied -= HandlePlayerDied;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMeteor), spawnDelay, spawnInterval);
    }

    private void HandlePlayerDied()
    {
        CancelInvoke();
    }

    private void HandleRegularMeteorDestroyed()
    {
        destroyedMeteorCount++;
        if (destroyedMeteorCount >= meteorsBeforeBigOne)
        {
            destroyedMeteorCount = 0;
            SpawnBigMeteor();
        }
    }

    private void SpawnMeteor()
    {
        Instantiate(meteorPrefab, RandomSpawnPosition(), Quaternion.identity);
    }

    private void SpawnBigMeteor()
    {
        Instantiate(bigMeteorPrefab, RandomSpawnPosition(), Quaternion.identity);
        GameEvents.RaiseBigMeteorSpawned();
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnY, 0f);
    }
}