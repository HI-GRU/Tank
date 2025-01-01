using UnityEngine;

public class TankTrailSpawner : TrailSpawner
{
    protected override void SpawnTrail()
    {
        GameObject tankTrail = ObjectPoolManager.Instance.SpawnFromPool(ObjectPoolManager.tankTrail, transform.position, transform.rotation);
    }
}