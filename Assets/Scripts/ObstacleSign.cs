using UnityEngine;

public class ObstacleSign : Sign
{
    protected override void Start()
    {
        base.Start();
        sign = ObjectPoolManager.Instance.SpawnFromPool(ObjectPoolManager.obstacleSign, transform.position, Quaternion.identity);
    }
}