using UnityEngine;

public class Missile : MonoBehaviour
{
    private LifeTimeController lifeTimeController;

    [Header("Missile Option")]
    [SerializeField] private float minLifeTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    private float lifeTime;
    private float speed;
    private float rotationSpeed;

    private void Awake()
    {
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
        speed = Random.Range(minSpeed, maxSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private void Start()
    {
        lifeTimeController = GetComponent<LifeTimeController>();
        StartCoroutine(lifeTimeController.LifetimeRoutine(lifeTime));
    }

    private void Update()
    {
        if (Player.Instance == null || lifeTimeController.isFading) return;
        Move();
    }

    private void FixedUpdate()
    {
        if (Player.Instance == null || lifeTimeController.isFading) return;
        SetDirection();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
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
}