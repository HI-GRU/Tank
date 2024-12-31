using UnityEngine;

public class Weapon : PooledObject
{

    [Header("Weapon Option")]
    [SerializeField] private float minLifeTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float speed;

    private void Update()
    {
        if (Player.Instance == null || isFading) return;
        Move();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForReturn());
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public override void InitializeObject()
    {
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
