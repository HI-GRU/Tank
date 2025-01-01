using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [Header("Trail Option")]
    [SerializeField] protected GameObject effectPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float duration;
    [SerializeField] private float fadeTime;

    private float timer;
    private List<GameObject> trails = new List<GameObject>();

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTrail();
            timer = 0F;
        }

        trails.RemoveAll(trail => trail == null);
    }

    protected virtual void SpawnTrail()
    {
        GameObject trail = Instantiate(effectPrefab, transform.position, transform.rotation);
        trails.Add(trail);
        LifeTimeController lifeTimeController = trail.AddComponent<LifeTimeController>();
        lifeTimeController.StartCoroutine(lifeTimeController.LifetimeRoutine(duration, fadeTime));
    }
}
