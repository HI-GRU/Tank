using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [Header("Spawning Option")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private int maxMissiles = 3;
    private float spawnInterval;
    private float spawnDistance;
    private float spawnTimer = 0F;

    private Camera mainCamera;
    private List<GameObject> missiles;

    private void Awake()
    {
        missiles = new List<GameObject>();
    }

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        spawnDistance = mainCamera.orthographicSize * 1.5F;
        spawnInterval = 3F;
    }

    private void Update()
    {
        if (Player.Instance == null) return;
        if (maxMissiles <= missiles.Count) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnMissile();
            spawnTimer = 0F;
            spawnInterval = Random.Range(1F, 5F);
        }
    }

    private void SpawnMissile()
    {
        float angle = Random.Range(0F, 360F);
        Vector2 spawnPosition = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * spawnDistance
         + (Vector2)Player.Instance.transform.position;

        Vector2 dir = ((Vector2)Player.Instance.transform.position - spawnPosition).normalized;
        float rotationAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90F;
        Quaternion rotation = Quaternion.Euler(0F, 0F, rotationAngle);

        GameObject missile = Instantiate(missilePrefab, spawnPosition, rotation);
        missiles.Add(missile);
    }
}
