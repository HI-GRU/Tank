
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [Header("Explosion Option")]
    [SerializeField] private GameObject explosionParticlePrefab;
    [SerializeField] private float playerExplosionSize;
    [SerializeField] private float weaponExplosionSize;
    [SerializeField] private float obstacleExplosionSize;

    Vector2 collisionPoint;
    private IPooledObject pooledObject;

    private void Start()
    {
        pooledObject = GetComponent<IPooledObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collisionPoint = other.ClosestPoint(transform.position);

        if (other.CompareTag("Player Skin") && GameManager.Instance.IsPointInCamera(collisionPoint))
        {
            HandlePlayerCollision(other);
            GameManager.Instance.SetCurrentState(GameManager.GameState.GameOver);
            return;
        }

        if (other.CompareTag("Weapon") && GameManager.Instance.IsPointInCamera(collisionPoint))
        {
            HandleWeaponCollision(other);
            ScoreManager.Instance.UpdateWeaponCollisionScore();
            return;
        }

        if (other.CompareTag("Obstacle Skin") && GameManager.Instance.IsPointInCamera(collisionPoint))
        {
            HandleObstacleCollision(other);
            return;
        }
    }

    private void HandlePlayerCollision(Collider2D playerSkin)
    {
        Explosion(playerExplosionSize);
        pooledObject.ReturnToPool();
        Destroy(playerSkin.GetComponentInParent<Player>().gameObject);
    }

    private void HandleWeaponCollision(Collider2D otherWeapon)
    {
        Explosion(weaponExplosionSize);
        pooledObject.ReturnToPool();
        otherWeapon.GetComponent<IPooledObject>().ReturnToPool();
    }

    private void HandleObstacleCollision(Collider2D obstacle)
    {
        Explosion(obstacleExplosionSize);
        pooledObject.ReturnToPool();
        obstacle.GetComponentInParent<Obstacle>().Damaged();
    }

    private void Explosion(float scale)
    {
        if (explosionParticlePrefab != null)
        {
            GameObject effect = Instantiate(explosionParticlePrefab, collisionPoint, Quaternion.identity);
            effect.transform.localScale = Vector3.one * scale;
        }
    }
}
