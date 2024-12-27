using UnityEngine;

public class SignController : MonoBehaviour
{
    [Header("Warning Option")]
    [SerializeField] private GameObject signPrefab;
    [SerializeField] private float offset;

    private GameObject sign;
    private bool isVisible = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        sign = Instantiate(signPrefab);
        sign.SetActive(isVisible);
    }

    private void Update()
    {
        if (!enabled || Player.Instance == null) return;
        UpdateSign();
    }

    private void UpdateSign()
    {
        if (!enabled || Player.Instance == null) return;

        bool setActive = !GameManager.Instance.IsPointInCamera(transform.position);
        if (isVisible != setActive)
        {
            sign.SetActive(setActive);
            isVisible = setActive;
        }

        if (!isVisible) return;
        UpdateSignPosition();
    }

    private void UpdateSignPosition()
    {
        Vector2 dir = (Vector2)(transform.position - Player.Instance.transform.position).normalized;
        Vector2 worldPos = transform.position;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float left = bottomLeft.x + offset;
        float right = topRight.x - offset;
        float bottom = bottomLeft.y + offset;
        float top = topRight.y - offset;

        Vector2 intersection = worldPos;

        if (dir.x != 0)
        {
            float t = dir.x > 0 ? (right - worldPos.x) / dir.x : (left - worldPos.x) / dir.x;
            Vector2 candidate = worldPos + t * dir;
            if (candidate.y >= bottom && candidate.y <= top)
                intersection = candidate;
        }

        if (dir.y != 0)
        {
            float t = dir.y > 0 ? (top - worldPos.y) / dir.y : (bottom - worldPos.y) / dir.y;
            Vector2 candidate = worldPos + t * dir;
            if (candidate.x >= left && candidate.x <= right)
                intersection = candidate;
        }

        sign.transform.position = intersection;
    }

    private void OnDestroy()
    {
        if (sign != null) Destroy(sign);
    }
}