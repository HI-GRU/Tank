using UnityEngine;

public class Obstacle : PooledObject
{
    [Header("Obstacle Option")]
    [SerializeField] private float minLifeTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private int maxHealth;
    [SerializeField] protected float bonusTime;
    [SerializeField] protected Sprite[] sprites;

    protected int health;
    protected bool isDestroyed;
    protected Coroutine lifeTimeCoroutine;

    public virtual void Damaged()
    {
        if (isDestroyed) return;

        if (--health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(FadeOut());
            return;
        }

        UpdateSprite();
        StartLifeTimeCoroutine();

        // 점수 로직
        ScoreManager.Instance.UpdateObstacleAttackScore(maxHealth - health);
    }

    public override void InitializeObject()
    {
        health = maxHealth;
        isDestroyed = false;
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
        UpdateSprite();
        StartLifeTimeCoroutine();
    }

    private void UpdateSprite()
    {
        if (health >= 0) spriteRenderer.sprite = sprites[health];
    }

    private void StartLifeTimeCoroutine()
    {
        if (lifeTimeCoroutine != null) StopCoroutine(lifeTimeCoroutine);
        lifeTimeCoroutine = StartCoroutine(WaitForReturn());
    }
}
