using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Update()
    {
        if (player == null) return;

        Vector3 pos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );

        transform.position = pos;
    }
}