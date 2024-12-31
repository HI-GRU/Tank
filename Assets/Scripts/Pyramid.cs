using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : Obstacle
{
    public override void Damaged()
    {
        base.Damaged();
        GameObject pyramid = ObjectPoolManager.Instance.SpawnFromPool(ObjectPoolManager.pyramidTags[health], transform.position, Quaternion.identity);

        if (pyramid != null)
        {
            Pyramid pyramidComponent = pyramid.GetComponent<Pyramid>();
            if (pyramidComponent != null)
            {
                pyramidComponent.SetCurrentHealth(health);

                if (health <= 0)
                {
                    isDestroyed = true;
                    pyramidComponent.SetDestroyTimer(2F);
                    ReturnToPool();
                    return;
                }

                pyramidComponent.SetDestroyTimer(lifeTime);
                ObstacleSpawner.Instance.AddToActiveObstacles(pyramid);
            }
        }

        ReturnToPool();
    }
}
