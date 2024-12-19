using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2F;  // 발사 간격 (초)
    [SerializeField] private float bulletSpeed = 5F;  // 총알 속도
    [SerializeField] private float rotationSpeed = 1F;

    private float fireTimer = 0F;

    private void Update()
    {
        if (Player.Instance == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Attack();
            fireTimer = 0F;
        }

        RotateEnemy();
    }

    private void Attack()
    {
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
