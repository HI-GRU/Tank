using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int health;
    private bool isDestroyed = false;
    private List<GameObject> obstacleType;
    private GameObject currentObject;

    public void Initialize(List<GameObject> type)
    {
        obstacleType = type;
        health = obstacleType.Count - 1;
        SetCurrentObject();
    }

    private void SetCurrentObject()
    {
        if (currentObject != null) Destroy(currentObject);

        if (health >= 0 && health < obstacleType.Count)
        {
            currentObject = Instantiate(obstacleType[health], transform);
            currentObject.transform.localPosition = Vector3.zero;
        }
    }

    public void Damaged()
    {
        if (isDestroyed) return;
        health--;

        SetCurrentObject();

        if (health == 0)
        {
            isDestroyed = true;
            LifeTimeController lifeTimeController = currentObject.AddComponent<LifeTimeController>();
            StartCoroutine(WaitForDestroy(lifeTimeController));
        }
    }

    private IEnumerator WaitForDestroy(LifeTimeController controller)
    {
        yield return StartCoroutine(controller.LifetimeRoutine(2F));
        Destroy(gameObject);
    }
}
