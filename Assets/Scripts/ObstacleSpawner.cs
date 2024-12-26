using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawning Option")]
    [SerializeField] private int maxObstacleCount;

    [Header("Obstacles Option")]
    [SerializeField] private int numOfTypes;
    [SerializeField] private List<GameObject> pyramids;
    // [SerializeField] private List<GameObject> sphinxes; // 추후 추가시 추가
    [SerializeField] private float minDistance;
    [SerializeField] private float destroyDuration;
    [SerializeField] private float destroyFadeTime;
    [SerializeField] private float lifeTime;
    [SerializeField] private float bonusTime;
    [SerializeField] private float spawnDistanceRate;

    private int type;
    private List<GameObject> currentType;
    private Camera mainCamera;
    private List<GameObject> obstacles;
    private float[] di = { -1, -1, 0, 1, 1, 1, 0, -1, 0 };
    private float[] dj = { 0, 1, 1, 1, 0, -1, -1, -1, 0 };
    private bool isSpawning = false;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        obstacles = new List<GameObject>();
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

        int spawnSide = Random.Range(0, 9);
        Vector2 spawnPosition = GetRandomPosition(spawnSide);

        if (!IsValidPosition(spawnPosition))
        {
            isSpawning = false;
            return;
        }

        GameObject obstaclObj = new GameObject("obstacle");
        obstaclObj.transform.position = spawnPosition;

        Obstacle obstacle = obstaclObj.AddComponent<Obstacle>();
        obstacle.Initialize(currentType, destroyDuration, destroyFadeTime, lifeTime, bonusTime);

        obstacles.Add(obstaclObj);

        isSpawning = false;
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

    private void SetType()
    {
        type = Random.Range(1, numOfTypes);

        switch (type)
        {
            case 1:
                currentType = pyramids;
                break;
            default:
                break;
        }
    }

    private bool CanSpawn()
    {
        obstacles.RemoveAll(obstacle => obstacle == null);
        return !isSpawning && obstacles.Count < maxObstacleCount;
    }
}
