using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Option")]
    [SerializeField] private int numOfTypes;
    [SerializeField] private List<GameObject> pyramids;
    private int health;
    private int type;
    private bool isDestroyed = false;
    private List<GameObject> currentType;
    private GameObject currentObject;
    private LifeTimeController lifeTimeController;

    private void Start()
    {
        SetType();
        health = currentType.Count - 1;
        SetCurrentObject();
    }

    private void SetCurrentObject()
    {
        if (currentObject != null) Destroy(currentObject);

        if (health >= 0 && health < currentType.Count)
        {
            currentObject = Instantiate(currentType[health], transform);
            currentObject.transform.localPosition = Vector3.zero;
        }
    }

    public void Damaged()
    {
        if (isDestroyed) return;

        SetCurrentObject();

        if (health == 0)
        {
            isDestroyed = true;
            StartCoroutine(lifeTimeController.LifetimeRoutine(2F));
        }
    }

    private void SetType()
    {
        type = Random.Range(1, numOfTypes);

        switch (type)
        {
            case 1:
                currentType = pyramids;
                break;
            default:
                break;
        }
    }
}
