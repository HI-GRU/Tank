using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [Header("Spawning Option")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private int minMissileCount;
    [SerializeField] private int maxMissileCount;
    private float spawnDistance;

    private Camera mainCamera;
    private List<GameObject> missiles;
    private int[] di = { -1, -1, 0, 1, 1, 1, 0, -1 };
    private int[] dj = { 0, 1, 1, 1, 0, -1, -1, -1 };
    private bool isSpawning = false;

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
            StartCoroutine(SpawnMissiles());
        }
    }

    private IEnumerator SpawnMissiles()
    {
        if (isSpawning) yield break;
        isSpawning = true;

        int spawnSide = Random.Range(0, 8);
        int cnt = Random.Range(minMissileCount, maxMissileCount + 1);

        while (cnt-- > 0)
        {
            Vector2 spawnPosition = GetRandomPosition(spawnSide);
            SpawnMissile(spawnPosition);
            float delay = Random.Range(0.5F, 1F);
            yield return new WaitForSeconds(delay);
        }

        isSpawning = false;
    }

    private Vector2 GetRandomPosition(int spawnSide)
    {
        float x = Random.Range(di[spawnSide], di[spawnSide] + 1);
        float y = Random.Range(dj[spawnSide], dj[spawnSide] + 1);

        return mainCamera.ViewportToWorldPoint(new Vector3(x, y, 0));
    }

    private void SpawnMissile(Vector2 position)
    {
        GameObject missile = Instantiate(missilePrefab, position, Quaternion.identity);
        missiles.Add(missile);
    }

    private bool CanSpawn()
    {
        missiles.RemoveAll(missile => missile == null);
        return !isSpawning && missiles.Count == 0;
    }
}
