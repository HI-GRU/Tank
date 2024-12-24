using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackDuration;
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player == null) return;
        if (other.CompareTag("Obstacle Skin"))
        {
            Vector2 knockbackDir = ((Vector2)transform.position - other.ClosestPoint(transform.position)).normalized;
            player.ApplyKnockback(knockbackDir * knockbackForce, knockbackDuration);
        }
    }
}
