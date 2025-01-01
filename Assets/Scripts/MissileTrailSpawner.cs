using UnityEngine;

public class MissileTrailSpawner : TrailSpawner
{
    protected override void SpawnTrail()
    {
        GameObject missileTrail = ObjectPoolManager.Instance.SpawnFromPool(ObjectPoolManager.weaponTrail, transform.position, transform.rotation);
    }
}