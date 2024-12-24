using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;

    private void Start()
    {
        if (Player.Instance == null) return;

        offset = transform.position - Player.Instance.transform.position;
    }

    private void Update()
    {
        if (Player.Instance == null) return;
        gameObject.transform.position = Player.Instance.transform.position + offset;
    }
}