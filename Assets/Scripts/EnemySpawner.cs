using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnDelay;
    public float repeatRate;

    public GameObject[] orcsPrefabs;
    [Space]

    public Transform[] spawnPoints;
    public Transform[] finishPoints;

    private void Start() => InvokeRepeating("SpawnEnemy", spawnDelay, repeatRate);

    private void SpawnEnemy()
    {
        var randomEnemy = RandomEnemy();
        var randomLine = Random.Range(0, spawnPoints.Length);
        int randomPosition = Random.Range(-1, 1);

        if (randomEnemy)
        {
            GameObject attacker = Instantiate(randomEnemy, spawnPoints[randomLine].position + 
                new Vector3(randomPosition * 0.5f, 0, 0), Quaternion.Euler(0, 180, 0));
            attacker.GetComponent<Enemy>().finishPoint = finishPoints[randomLine];
            attacker.GetComponent<Enemy>().activeLine = randomLine;
        }
    }

    private GameObject RandomEnemy()
    {
        GameObject setPrefab;
        if (GameStats.waveCounter == 1)
        {
            return setPrefab = orcsPrefabs[Random.Range(0, 3)];
        }
        else if (GameStats.waveCounter == 2)
        {
            return setPrefab = orcsPrefabs[Random.Range(0, 7)];
        }
        else if (GameStats.waveCounter == 3)
        {
            return setPrefab = orcsPrefabs[Random.Range(4, 9)];
        }
        else
        {
            return null;
        }
    }
}