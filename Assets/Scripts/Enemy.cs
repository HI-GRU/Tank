using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Option")]
    // [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2F;
    [SerializeField] private float bulletSpeed = 5F;
    [SerializeField] private float rotationSpeed = 1F;

    private float fireTimer = 0F;

    [Header("Alive Option")]
    [SerializeField] private float lifeTime = 20F;
    [SerializeField] private float fadeTime = 1F;
    private bool isFading = false;

    private void Start()
    {
        StartCoroutine(LifetimeRoutine());
    }

    private void Update()
    {
        if (Player.Instance == null || isFading) return;

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

    private IEnumerator LifetimeRoutine()
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
