using System;
using UnityEngine;

[Obsolete("Enemy Deleted")]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Option")]
    // [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2F;
    [SerializeField] private float rotationSpeed = 1F;

    private float fireTimer = 0F;

    [Header("Alive Option")]
    [SerializeField] private float lifeTime = 20F;
    private LifeTimeController lifeTimeController;

    private void Start()
    {
        lifeTimeController = GetComponent<LifeTimeController>();
        StartCoroutine(lifeTimeController.LifetimeRoutine(lifeTime));
    }

    private void Update()
    {
        if (Player.Instance == null || lifeTimeController.isFading) return;

        RotateEnemy();

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Attack();
            fireTimer = 0F;
        }
    }

    private void Attack()
    {
        // TODO: 총알 발사 로직 구현
        Debug.Log("Fire !!!");
    }

    private void RotateEnemy()
    {
        Vector2 dir = (Player.Instance.transform.position - gameObject.transform.position).normalized;

        float currentAngle = transform.eulerAngles.z;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90F;
        float angle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0F, 0F, angle);
    }
}
