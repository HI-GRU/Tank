using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [Header("Spawning Option")]
    [SerializeField] private GameObject missilePrefab;
    private float spawnDistance;

    private Camera mainCamera;
    private List<GameObject> missiles;

    private void Awake()
    {
        missiles = new List<GameObject>();
    }

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        spawnDistance = mainCamera.orthographicSize * 3F;
    }

    private void Update()
    {
        if (Player.Instance == null) return;

        if (CanSpawn())
        {
            int rand = Random.Range(1, 4);
            while (rand-- > 0) SpawnMissile();
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

    private bool CanSpawn()
    {
        missiles.RemoveAll(missile => missile == null);
        return missiles.Count == 0;
    }
}
