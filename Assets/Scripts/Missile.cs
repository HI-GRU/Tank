using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float lifeTime;
    private LifeTimeController lifeTimeController;

    private void Awake()
    {
        lifeTime = Random.Range(10F, 20F);
    }

    private void Start()
    {
        lifeTimeController = GetComponent<LifeTimeController>();
        StartCoroutine(lifeTimeController.LifetimeRoutine(lifeTime));
    }

    private void Update()
    {
        if (Player.Instance == null || lifeTimeController.isFading) return;
        
    }
}
