
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    [Header("Explosion Option")]
    [SerializeField] private GameObject explosionParticlePrefab;
    [SerializeField] private float playerExplosionSize;
    [SerializeField] private float weaponExplosionSize;
    [SerializeField] private float obstacleExplosionSize;

    Vector2 collisionPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        collisionPoint = other.ClosestPoint(transform.position);

        if (other.CompareTag("Player Skin"))
        {
            HandlePlayerCollision(other);
            return;
        }

        if (other.CompareTag("Weapon"))
        {
            HandleWeaponCollision(other);
            return;
        }

        if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision(other);
            return;
        }
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
