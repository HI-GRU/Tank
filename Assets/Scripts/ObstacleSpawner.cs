using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private static ObstacleSpawner instance;
    public static ObstacleSpawner Instance => instance;

    [Header("Spawning Option")]
    [SerializeField] private int maxObstacleCount;
    [SerializeField] private float minDistance;
    [SerializeField] private float spawnDistanceRate;
    [SerializeField] private int numOfTypes;

    private string type;
    private Camera mainCamera;
    private List<GameObject> activeObstacles;
    private float[] di = { -1, -1, 0, 1, 1, 1, 0, -1, 0 };
    private float[] dj = { 0, 1, 1, 1, 0, -1, -1, -1, 0 };
    private bool isSpawning = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        activeObstacles = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (Player.Instance == null) return;
        if (CanSpawn()) SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        if (isSpawning) return;
        isSpawning = true;

        SetType();
        int spawnSide = Random.Range(0, 8);
        Vector2 spawnPosition = GetRandomPosition(spawnSide);

        if (!IsValidPosition(spawnPosition))
        {
            isSpawning = false;
            return;
        }

        GameObject obstacle = ObjectPoolManager.Instance.SpawnFromPool(type, spawnPosition, Quaternion.identity);
        AddToActiveObstacles(obstacle);

        isSpawning = false;
    }

    private void SetType()
    {
        int ty = Random.Range(1, numOfTypes + 1);

        switch (ty)
        {
            case 1:
                List<string> list = ObjectPoolManager.pyramidTags;
                type = list[list.Count - 1];
                break;

            default:
                break;
        }
    }

    private Vector2 GetRandomPosition(int spawnSide)
    {
        float x = Random.Range(di[spawnSide], di[spawnSide] + 1);
        float y = Random.Range(dj[spawnSide], dj[spawnSide] + 1);

        return mainCamera.ViewportToWorldPoint(new Vector3(x, y, 0) * spawnDistanceRate);
    }

    private bool IsValidPosition(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);
        return colliders.Length == 0;
    }

    private bool CanSpawn()
    {
        activeObstacles.RemoveAll(obstacle => obstacle == null || !obstacle.activeInHierarchy);
        return !isSpawning && activeObstacles.Count < maxObstacleCount;
    }

    public void AddToActiveObstacles(GameObject obstacle)
    {
        if (obstacle != null && !activeObstacles.Contains(obstacle))
        {
            activeObstacles.Add(obstacle);
        }
    }
}
