using UnityEngine;

public class CameraController : MonoBehaviour
{
    // public float lerpSpeed = 1.0F;
    private Vector3 offset;
    // private Vector3 targetPos;

    private void Start()
    {
        if (Player.Instance == null) return;

        offset = transform.position - Player.Instance.transform.position;
    }

    private void Update()
    {
        if (Player.Instance == null) return;

        // targetPos = Player.Instance.transform.position + offset;
        // transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);

        gameObject.transform.position = Player.Instance.transform.position + offset;
    }
}