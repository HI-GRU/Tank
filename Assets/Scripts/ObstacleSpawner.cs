using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacles Option")]
    [SerializeField] private int numOfTypes;
    [SerializeField] private List<GameObject> pyramids;
    private int type;
    private List<GameObject> currentType;

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
