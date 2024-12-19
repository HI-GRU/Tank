using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeController : MonoBehaviour
{
    public bool isFading;
    private float fadeTime = 1F;

    public IEnumerator LifetimeRoutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        isFading = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        float elapsedTime = 0F;
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

        Destroy(gameObject);
    }
}
