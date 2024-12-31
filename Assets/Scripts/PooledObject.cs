using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour, IPooledObject
{
    [Header("Pool Option")]
    [SerializeField] protected float fadeTime = 1F;
    public string poolTag;
    protected float lifeTime;
    protected bool isFading;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void InitializeObject();

    protected IEnumerator WaitForReturn()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(FadeOut());
    }

    protected IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0F;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;
            spriteRenderer.color = new Color(
                originalColor.r,
                originalColor.g,
                originalColor.b,
                Mathf.Lerp(1, 0, normalizedTime)
            );
            yield return null;
        }
        ReturnToPool();
    }

    public virtual void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnToPool(poolTag, gameObject);
    }
}
