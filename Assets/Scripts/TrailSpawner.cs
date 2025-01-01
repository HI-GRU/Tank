using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrailSpawner : MonoBehaviour
{
    [Header("Trail Option")]
    [SerializeField] private float spawnInterval;

    private float timer;

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTrail();
            timer = 0F;
        }
    }

    protected abstract void SpawnTrail();
}
