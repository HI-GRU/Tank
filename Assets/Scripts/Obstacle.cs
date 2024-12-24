using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int health;
    private bool isDestroyed = false;
    private List<GameObject> obstacleType;
    private GameObject currentObject;
    private LifeTimeController lifeTimeController;
    private float destroyDuration;
    private float destroyFadeTime;
    private float lifeTime;
    private float bonusTime;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void Initialize(List<GameObject> type, float destroyDuration, float destroyFadeTime, float lifeTime, float bonusTime)
    {
        obstacleType = type;
        health = obstacleType.Count - 1;
        this.destroyDuration = destroyDuration;
        this.destroyFadeTime = destroyFadeTime;
        this.lifeTime = lifeTime;
        this.bonusTime = bonusTime;
        SetCurrentObject();
    }

    private void SetCurrentObject()
    {
        if (currentObject != null)
        {
            StopAllCoroutines();
            Destroy(currentObject);
        }

        if (health >= 0 && health < obstacleType.Count)
        {
            currentObject = Instantiate(obstacleType[health], transform);
            currentObject.transform.localPosition = Vector3.zero;
            lifeTimeController = currentObject.AddComponent<LifeTimeController>();

            float remainingTime;
            if (health == 0 && isDestroyed) remainingTime = destroyDuration;
            else remainingTime = lifeTime - timer;

            StartCoroutine(WaitForDestroy(lifeTimeController, remainingTime, destroyFadeTime));
        }
    }

    public void Damaged()
    {
        if (isDestroyed) return;
        if (--health == 0) isDestroyed = true;
        else lifeTime += bonusTime;
        SetCurrentObject();
    }

    private IEnumerator WaitForDestroy(LifeTimeController controller, float duration, float fadeTime)
    {
        yield return StartCoroutine(controller.LifetimeRoutine(duration, fadeTime));
        Destroy(gameObject);
    }
}
