
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    [Header("Explosion Option")]
    [SerializeField] private GameObject explosionParticlePrefab;
    [SerializeField] private float playerExplosionSize;
    [SerializeField] private float weaponExplosionSize;

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
    }

    private void HandlePlayerCollision(Collider2D playerSkin)
    {
        Explosion(collisionPoint, playerExplosionSize);
        Destroy(playerSkin.GetComponentInParent<Player>().gameObject);
        Destroy(gameObject);
    }

    private void HandleWeaponCollision(Collider2D otherWeapon)
    {
        Explosion(collisionPoint, weaponExplosionSize);
        Destroy(otherWeapon.gameObject);
        Destroy(gameObject);
    }

    private void Explosion(Vector3 position, float scale)
    {
        if (explosionParticlePrefab != null)
        {
            GameObject effect = Instantiate(explosionParticlePrefab, position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * scale;
        }
    }
}
