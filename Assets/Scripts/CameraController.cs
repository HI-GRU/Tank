using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 10F;

    private void Update()
    {
        if (player == null) return;

        Vector3 pos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            pos,
            speed * Time.deltaTime
        );
    }
}