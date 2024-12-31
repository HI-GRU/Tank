using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : PooledObject
{
    [Header("Obstacle Option")]
    [SerializeField] private float minLifeTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private int maxHealth;
    [SerializeField] protected float bonusTime;

    protected int health;
    protected bool isDestroyed;
    protected bool isFirstSpawn = true;

    public virtual void Damaged()
    {
        if (isDestroyed) return;

        health--;
        ScoreManager.Instance.UpdateObstacleAttackScore(maxHealth - health);
    }

    public override void InitializeObject()
    {
        if (isFirstSpawn)
        {
            health = maxHealth;
            isDestroyed = false;
            lifeTime = Random.Range(minLifeTime, maxLifeTime);
            isFirstSpawn = false;
        }
        else
        {
            lifeTime += bonusTime;
        }
    }

    public virtual void SetCurrentHealth(int currentHealth)
    {
        health = currentHealth;
    }

    public virtual void SetDestroyTimer(float time)
    {
        lifeTime = time;
        StartCoroutine(WaitForReturn());
    }
}
