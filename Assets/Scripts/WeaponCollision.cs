
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [Header("Explosion Option")]
    [SerializeField] private GameObject explosionParticlePrefab;
    [SerializeField] private float playerExplosionSize;
    [SerializeField] private float weaponExplosionSize;
    [SerializeField] private float obstacleExplosionSize;

    private Camera mainCamera;
    Vector2 collisionPoint;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collisionPoint = other.ClosestPoint(transform.position);

        if (other.CompareTag("Player Skin") && IsPointInCamera(collisionPoint))
        {
            HandlePlayerCollision(other);
            return;
        }

        if (other.CompareTag("Weapon") && IsPointInCamera(collisionPoint))
        {
            HandleWeaponCollision(other);
            return;
        }

        if (other.CompareTag("Obstacle Skin") && IsPointInCamera(collisionPoint))
        {
            HandleObstacleCollision(other);
            return;
        }
    }

    private bool IsPointInCamera(Vector2 point)
    {
        Vector2 viewport = mainCamera.WorldToViewportPoint(point);
        return viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1;
    }

    private void HandlePlayerCollision(Collider2D playerSkin)
    {
        Explosion(playerExplosionSize);
        Destroy(gameObject);
        Destroy(playerSkin.GetComponentInParent<Player>().gameObject);
    }

    private void HandleWeaponCollision(Collider2D otherWeapon)
    {
        Explosion(weaponExplosionSize);
        Destroy(gameObject);
        Destroy(otherWeapon.gameObject);
    }

    private void HandleObstacleCollision(Collider2D obstacle)
    {
        Explosion(obstacleExplosionSize);
        Destroy(gameObject);
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
