using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    
    private int enemiesAlive;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown;

    [Header("Enemy Limit")]
    [SerializeField] private int maxEnemies = 10;

    [Space]
    [SerializeField] private float cooldownDecreaseRate = 0.5f;
    [SerializeField] private float coolDownCap = .7f;

    private float timer;
    private int enemiesSpawned;

    private Transform player;

    private void Awake()
    {
        player = FindFirstObjectByType<player>()?.transform;
    }

    private void Update()
    {
        // Stop spawning after reaching the limit
        if (enemiesSpawned >= maxEnemies)
            return;

        // Find player again if the reference was destroyed
        if (player == null)
        {
            player = FindFirstObjectByType<player>()?.transform;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = cooldown;

            CreateNewEnemy();

            cooldown = Mathf.Max(
                coolDownCap,
                cooldown - cooldownDecreaseRate
            );
        }
    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(
            0,
            respawnPoints.Length
        );

        Transform spawnPoint = respawnPoints[respawnPointIndex];

        Debug.Log("Spawn point: " + spawnPoint.name);
        Debug.Log("Spawn position: " + spawnPoint.position);

        GameObject newEnemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        enemiesSpawned++;
        enemiesAlive++;

        Enemy enemy = newEnemy.GetComponent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("Enemy component NOT FOUND on spawned prefab!");
            return;
        }

        // Give the enemy a reference to this respawner
        enemy.SetRespawner(this);

        if (player != null)
        {
            bool createdOfTheRight =
                newEnemy.transform.position.x > player.position.x;

            if (createdOfTheRight)
            {
                enemy.flip();
            }
        }
    }
    public void EnemyDied()
    {
        enemiesAlive--;

        Debug.Log(
        "Enemy died | Spawned: " + enemiesSpawned +
        " | Alive: " + enemiesAlive +
        " | Max: " + maxEnemies
    );

        if (enemiesSpawned == maxEnemies && enemiesAlive == 0)
        {
            UI.instance.EnableVictoryUI();
        }
    }
}