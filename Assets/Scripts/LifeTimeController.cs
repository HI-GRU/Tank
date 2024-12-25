using System.Collections;
using UnityEngine;

public class LifeTimeController : MonoBehaviour
{
    public bool isFading;

    public IEnumerator LifetimeRoutine(float lifeTime, float fadeTime)
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
