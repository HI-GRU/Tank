using UnityEngine;

public class Missile : Weapon
{
    [Header("Missile Option")]
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    private float rotationSpeed;

    private void FixedUpdate()
    {
        if (Player.Instance == null || !isFading) return;
        SetDirection();
    }

    private void SetDirection()
    {
        // TODO: 방향 조정에 어느정도 랜덤값 부여 ?
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90F;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0F, 0F, angle),
            rotationSpeed * Time.fixedDeltaTime
        );
    }

    public override void InitializeObject()
    {
        base.InitializeObject();
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }
}