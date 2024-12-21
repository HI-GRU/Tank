
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
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
        Destroy(playerSkin.GetComponentInParent<Player>().gameObject);
        Destroy(gameObject);
    }

    private void HandleWeaponCollision(Collider2D otherWeapon)
    {
        Destroy(otherWeapon.gameObject);
        Destroy(gameObject);
    }
}
