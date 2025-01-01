using System.Collections.Generic;
using UnityEngine;

public class Trail : PooledObject
{
    [SerializeField] private float trailLifeTime;

    public override void InitializeObject()
    {
        lifeTime = trailLifeTime;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForReturn());
    }
}
