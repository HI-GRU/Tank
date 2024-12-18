using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public float lerpSpeed = 1.0F;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Start()
    {
        if (player == null) return;

        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (player == null) return;

        targetPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}