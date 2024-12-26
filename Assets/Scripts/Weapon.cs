using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected LifeTimeController lifeTimeController;

    [Header("Weapon Option")]
    [SerializeField] private float minLifeTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float fadeTime;
    private float lifeTime;
    private float speed;

    protected virtual void Awake()
    {
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    protected void Start()
    {
        lifeTimeController = GetComponent<LifeTimeController>();
        StartCoroutine(lifeTimeController.LifetimeRoutine(lifeTime, fadeTime));
    }

    private void Update()
    {
        if (Player.Instance == null || lifeTimeController.isFading) return;
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
