using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Obsolete("Enemy Deleted")]
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning Option")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float distanceRate = 0.3F;
    [SerializeField] private int minEnemies = 1;
    [SerializeField] private int maxEnemies = 4;

    private List<GameObject> enemies;
    private int[] di = { -1, -1, 0, 1, 1, 1, 0, -1 };
    private int[] dj = { 0, 1, 1, 1, 0, -1, -1, -1 };
    private bool isSpawning = false;

    private Camera mainCamera;
    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        enemies = new List<GameObject>();
    }

    private void Update()
    {
        if (CanSpawn())
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (isSpawning) return;
        isSpawning = true;

        int spawnSide = Random.Range(0, 8);
        int size = Random.Range(minEnemies, maxEnemies);

        while (size-- > 0)
        {
            Vector2 spawnPosition = GetRandomPosition(spawnSide);
            SpawnEnemy(spawnPosition);
        }

        isSpawning = false;
    }
    private Vector2 GetRandomPosition(int spawnSide)
    {
        float x = Random.Range(di[spawnSide] + distanceRate, di[spawnSide] + 1 - distanceRate);
        float y = Random.Range(dj[spawnSide] + distanceRate, dj[spawnSide] + 1 - distanceRate);

        return mainCamera.ViewportToWorldPoint(new Vector3(x, y, 0));
    }

    private void SpawnEnemy(Vector2 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemies.Add(enemy);
    }

    private bool CanSpawn()
    {
        enemies.RemoveAll(enemy => enemy == null);
        return !isSpawning && enemies.Count == 0;
    }
}