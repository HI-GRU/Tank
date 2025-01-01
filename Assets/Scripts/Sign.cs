using System.Collections;
using UnityEngine;

public class Sign : PooledObject
{
    [Header("Warning Option")]
    [SerializeField] private float offset;

    protected GameObject sign;
    protected GameObject target;
    private bool isVisible;
    private Camera mainCamera;

    protected virtual void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        target = gameObject;
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

    protected void UpdateSignPosition()
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

    public override void InitializeObject()
    {
        isVisible = false;
    }
}