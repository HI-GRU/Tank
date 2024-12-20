using UnityEngine;

public class Missile : MonoBehaviour
{
    private float lifeTime;
    private LifeTimeController lifeTimeController;

    [Header("Missile Option")]
    [SerializeField] private float speed = 10F;
    [SerializeField] private float rotationSpeed = 180F;

    private void Awake()
    {
        lifeTime = Random.Range(10F, 15F);
    }

    private void Start()
    {
        lifeTimeController = GetComponent<LifeTimeController>();
        StartCoroutine(lifeTimeController.LifetimeRoutine(lifeTime));
    }

    private void FixedUpdate()
    {
        if (Player.Instance == null || lifeTimeController.isFading) return;
        Attack();
    }

    private void Attack()
    {
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90F;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0F, 0F, angle),
            rotationSpeed * Time.fixedDeltaTime
        );

        transform.position += transform.up * speed * Time.fixedDeltaTime;
    }
}