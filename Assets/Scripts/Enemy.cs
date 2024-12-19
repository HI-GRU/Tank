using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2f;  // 발사 간격 (초)
    [SerializeField] private float bulletSpeed = 5f;  // 총알 속도

    private float fireTimer = 0f;

    private void Update()
    {
        // 플레이어가 없으면 발사하지 않음
        if (Player.Instance == null) return;

        // 발사 타이머 업데이트
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            // FireBullet();
            fireTimer = 0f;
        }
    }

    // private void FireBullet()
    // {
    //     // 현재 플레이어 위치를 가져와서 방향 계산
    //     Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;

    //     // 총알 생성 및 초기화
    //     GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    //     Bullet bulletComponent = bullet.GetComponent<Bullet>();
    //     if (bulletComponent != null)
    //     {
    //         bulletComponent.Initialize(direction, bulletSpeed);
    //     }
    // }
}
